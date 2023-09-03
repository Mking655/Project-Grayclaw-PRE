using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoxyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform orgin;
    List<Vent> vents;
    [SerializeField]
    private float speed = 9;
    [SerializeField]
    private float minWaitTime, maxWaitTime;



    private void idle()
    {

    }
    private void ping()
    {

    }
    private void attack()
    {

    }
}
