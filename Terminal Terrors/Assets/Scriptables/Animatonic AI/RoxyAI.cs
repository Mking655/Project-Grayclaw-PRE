using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class RoxyAI : MonoBehaviour
{
    public enum State
    {
        idling,
        pinging,
        attacking
    }
    private GameStateManager playerStateManager;
    private State currentState;
    public State getCurrentState()
    {
        return currentState;
    }
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private List<Vent> vents;
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private float speed = 9;
    [SerializeField]
    private float minWaitTime, maxWaitTime;
    [SerializeField]
    private float pingTime;
    [SerializeField]
    private GameObject pingCountdownUI;
    [SerializeField]
    private AudioClip foundYouSound;
    //for readability
    private void setState(RoxyAI.State state)
    {
        currentState = state;
    }
    public float getPingTime()
    {
        return pingTime;
    }
    private float idleTime;
    private bool hasStartedPing;
    private bool hasStartedAttacking;
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        gameObject.GetComponent<AudioSource>().Stop();
    }

    void Move()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        //Navigation Setup
        navMeshAgent = GetComponent<NavMeshAgent>();
        Move();
        navMeshAgent.SetDestination(origin.transform.position);
        
        //Assuming only one per scene
        playerStateManager = FindFirstObjectByType<GameStateManager>();
        
        // Initialize variables
        idleTime = Random.Range(minWaitTime, maxWaitTime);

        setState(RoxyAI.State.idling);
    }

    private void Update()
    {
        switch (currentState)
        {
            case RoxyAI.State.idling:
                idle();
                break;
            case RoxyAI.State.pinging:
                ping();
                break;
            case RoxyAI.State.attacking:
                attack();
                break;
        }
    }

    private void idle()
    {
        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            setState(RoxyAI.State.pinging);
            idleTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    private void ping()
    {
        //Start state
        if (!hasStartedPing)
        {
            hasStartedPing = true;
            Debug.Log("Starting Ping");
            StartCoroutine(PingCooldown());
        }
    }
    //walking state player must always be named "--Walking Player"
    private IEnumerator PingCooldown()
    {
        pingCountdownUI.SetActive(true);
        yield return new WaitForSeconds(pingTime);
        //if player is moving and is in FPS mode
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && playerStateManager.getActiveState().gameObject.name == "--Walking Player")
        {
            Debug.Log("Ping detected movement");
            setState(RoxyAI.State.attacking);
        }
        else
        {
            Debug.Log("No movement detected");
            setState(RoxyAI.State.idling);
        }
        hasStartedPing = false;
    }
    private Vent findClosestVent()
    {
        foreach(Vent vent in vents)
        {
            if(vent.playerInRoom) 
            { 
                return vent;
            }
        }
        return null;
    }
    private void attack()
    {
        //Start state
        if (!hasStartedAttacking)
        {
            hasStartedAttacking = true;
            if (findClosestVent() != null)
            {
                Debug.Log("You're cooked");
                Move();
                Vent closestVent = findClosestVent();
                navMeshAgent.SetDestination(closestVent.transform.position);

            }
            else
            {
                Debug.Log("You can't hide forever");
                setState(RoxyAI.State.idling);
                hasStartedAttacking = false;
            }
        }
    }
}