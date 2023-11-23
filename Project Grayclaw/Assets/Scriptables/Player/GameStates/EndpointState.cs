using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointState : GameState
{
    [SerializeField]
    public GameState walkingState;
    [SerializeField]
    private Camera cam;
    /// <summary>
    /// Takes player back to walking state from interacting state.
    /// </summary>
    public void exitInterface()
    {
        getManager().changeState(walkingState);
    }
    //TODO: Should every prefab have to define this? Does the interface always exit to walking?

    //Unlock cursor so player can interact
    public override void activate()
    {
        base.activate();
        Cursor.lockState = CursorLockMode.None;
        cam.fieldOfView = PlayerSettings.InteractingFOV;
    }
}
