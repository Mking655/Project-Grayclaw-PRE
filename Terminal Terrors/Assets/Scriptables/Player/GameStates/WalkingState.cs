using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkingState : GameState
{
    //assumes use of FPS controller prefab.
    [SerializeField]
    private GameObject FPSController;
    public override void activate()
    {
        base.activate();
        FPSController.SetActive(true);
    }
}
