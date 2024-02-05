using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Prompter))]
[RequireComponent(typeof(ComputerPOV))]
/// <summary>
/// Point that bings the interaction controller to its location when called
/// </summary>
public class InteractionPoint : MonoBehaviour
{
    [HideInInspector]
    public InteractionController interactionController;
    public void Call()
    {
        if (interactionController == null) 
        {
            Debug.LogWarning(gameObject.name + " has no defined interaction controller.");
            return;
        }
        interactionController.changeActivePoint(this as  InteractionPoint);
    }
}
