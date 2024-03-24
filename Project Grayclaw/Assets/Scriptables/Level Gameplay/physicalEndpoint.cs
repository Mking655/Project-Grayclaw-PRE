using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// The physical, in level instance of the endpoint. Handles in level function.
/// </summary>
/// TODO: give a relevant tag for ai detection.
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(mapInfo))]
public class physicalEndpoint : MonoBehaviour
{
    //linked endpoint
    public Endpoint endpoint;
    [HideInInspector]
    public Animator animator;//Have physical representation change according to state
    //Injected by corresponding room
    [HideInInspector]
    public Room room;
    public UnityEvent onBreak;
    //bool isBroken = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (endpoint == null)
        {
            Debug.Log(gameObject.name + " has not been linked to an endpoint on the core computer canvas");
        }
    }
    private void Start()
    {
        if(room == null)
        {
            Debug.LogError("Physical endpoint without a corresponding room");
        }
    }
    //with this system, the only way the endpoint can be broken is through the physical endpoint.
    //This physical endpoint will not update to the Endpoint script's state.
    //This should be fine, as the only case where the script updates itself is when the player patches the endpoint,
    //which should have no physical reprsentation.
    public void breakEndpoint()
    {
        if(endpoint.state == EndpointState.Fixed) 
        {
            Debug.Log("cannot break a fixed endpoint");
            return;
        }
        animator.SetBool("isBroken", true);
        endpoint.ChangeState(EndpointState.Broken);
        onBreak.Invoke();
        //isBroken = true;
    }
    public void fixEndpoint()
    {
        if(endpoint.state == EndpointState.Broken)
        {
            animator.SetBool("isBroken", false);
            endpoint.ChangeState(EndpointState.Vulnerable);
            //isBroken = false;
        }
        else
        {
            Debug.Log("endpoint not broken, cannot fix");
        }
    }
}
