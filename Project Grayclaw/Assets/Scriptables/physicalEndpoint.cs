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
public class physicalEndpoint : MonoBehaviour
{
    [HideInInspector]
    public Endpoint endpoint;//injected refrence to corresponding endpoint
    [HideInInspector]
    public Animator animator;//Have physical representation change according to state
    //TODO: possible room detection and automatic assignment into room? What would a "room" script be?
    public UnityEvent onBreak;
    bool isBroken = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void breakEndpoint()
    {
        animator.SetBool("isBroken", true);
        onBreak.Invoke();
        isBroken = true;
    }
    public void fixEndpoint()
    {
        animator.SetBool("isBroken", true);
        isBroken = false;
    }
}
