using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoxyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
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
    public float getPingTime()
    {
        return pingTime;
    }
    private float idleTime;
    private bool isIdling;
    public bool getIdling()
    {
        return isIdling;
    }
    private bool isPinging;
    public bool getPinging()
    {
        return isPinging;
    }
    private bool hasStartedPing;
    private bool isAttacking;
    public bool getAttacking()
    {
        return isAttacking;
    }

    private void Start()
    {
        // Initialize variables
        idleTime = Random.Range(minWaitTime, maxWaitTime);
        isIdling = true;
        isPinging = false;
        isAttacking = false;
    }

    private void Update()
    {
        if (isIdling)
        {
            idle();
        }
        if (isPinging)
        {
            ping();
        }
        if (isAttacking)
        {
            attack();
        }
    }

    private void idle()
    {
        idleTime -= Time.deltaTime;

        if (idleTime <= 0)
        {
            isIdling = false;
            isPinging = true;
            idleTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    private void ping()
    {
        if (!hasStartedPing)
        {
            hasStartedPing = true;
            Debug.Log("Starting Ping");
            StartCoroutine(PingCooldown());
        }
    }

    private IEnumerator PingCooldown()
    {
        pingCountdownUI.SetActive(true);
        yield return new WaitForSeconds(pingTime);
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("Ping detected movement");
            isIdling = false;
            isPinging = false;
            isAttacking = true;
        }
        else
        {
            Debug.Log("No movement detected");
            isIdling = true;
            isPinging = false;
            isAttacking = false;
        }
        hasStartedPing = false;
    }

    private void attack()
    {
        Debug.Log("you are going to be dead");
        // Implement logic here
    }
}