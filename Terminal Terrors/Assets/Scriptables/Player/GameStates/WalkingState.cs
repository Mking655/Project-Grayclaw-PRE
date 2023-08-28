using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkingState : GameState
{
    public static int walkingFOV = 80;
    [SerializeField]
    private Camera cam;
    //assumes use of FPS controller prefab.
    [SerializeField]
    private GameObject FPSController;
    public override void activate()
    {
        base.activate();
        FPSController.SetActive(true);
        //lock cursor in fps mode
        Cursor.lockState = CursorLockMode.Locked;
        cam.fieldOfView = walkingFOV;
    }
    public override void deactivate() 
    { 
        base.deactivate();
        FPSController.SetActive(false);
    }
}
