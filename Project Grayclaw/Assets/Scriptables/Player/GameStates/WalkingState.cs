using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkingState : GameState
{
    [SerializeField]
    private Camera cam;
    //assumes use of FPS controller prefab.
    [SerializeField]
    private GameObject FPSController;
    public override void activate()
    {
        base.activate();
        //lock cursor in fps mode
        Cursor.lockState = CursorLockMode.Locked;
        cam.fieldOfView = PlayerSettings.FOV;
    }
    public override void deactivate() 
    { 
        base.deactivate();
    }
}
