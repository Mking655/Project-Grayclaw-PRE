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
    private float ventCheckTime;
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
        navMeshAgent.SetDestination(origin.transform.position);
        navMeshAgent.stoppingDistance = 0;

        //Assuming only one per scene
        playerStateManager = FindFirstObjectByType<GameStateManager>();

        // Initialize variables
        idleTime = Random.Range(minWaitTime, maxWaitTime);
        Move();
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
    //must be acomanied by "Move()" before call
    private void idle()
    {
        navMeshAgent.SetDestination(origin.transform.position);
        Debug.Log(navMeshAgent.remainingDistance);
        if(navMeshAgent.pathPending == false && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Stop();
        }

        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            Stop();
            setState(RoxyAI.State.pinging);
            idleTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    private void ping()
    {
        Stop();
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
            Move();
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
    IEnumerator startAttack()
    {
        Move();
        navMeshAgent.SetDestination(findClosestVent().transform.position);
        Vent closestVent = findClosestVent();
        Debug.Log("Starting Attack on " + closestVent.roomName);
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            Debug.Log(navMeshAgent.remainingDistance + " meters until she reaches you");
            yield return null;
        }
        Stop();
        //Will keep checking on update call until end of time limit
        float remainingTime = ventCheckTime;
        while(remainingTime > 0) 
        {
            bool caught = closestVent.check();
            if (caught)
            {
                Debug.Log("You have been caught");
            }
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        Move();
        setState(RoxyAI.State.idling);
        hasStartedAttacking = false;
        closestVent.leave();
    }
    private void attack()
    {
        //Start state
        if (!hasStartedAttacking)
        {
            hasStartedAttacking = true;
            if (findClosestVent() != null)
            {
                StartCoroutine(startAttack());
            }
            else
            {
                Debug.Log("You can't hide forever");
                //exit this state
                Move();
                setState(RoxyAI.State.idling);
                hasStartedAttacking = false;
            }
        }
    }
}