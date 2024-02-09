using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class POVManager : Singleton<POVManager>
{
    [SerializeField]
    private POV startingPOV;//choose what state player should be in
    [SerializeField]
    public PlayerCamera cam;//define in inspector
    [HideInInspector]
    public List<POV> POVs = new List<POV>();//TODO populate automaticly on start
    [HideInInspector]
    public POV previousActiveState;
    private POV activeState;
    private void Start()
    {
        //TODO
        POVs = new List<POV>(FindObjectsByType<POV>(FindObjectsSortMode.None));
        previousActiveState = null;
        if (startingPOV == null)
        {
            Debug.LogWarning("No starting POV for game. Defaulting to 0,0,0");
            cam.followPoint = cam.transform;
            activeState = null;
            return;
        }
        changeState(startingPOV);
    }
    public POV getActiveState()
    {
        return activeState;
    }
    /// <summary>
    /// Changes the player's POV(gamemode) by enumerating through all given POVs.
    /// </summary>
    public void changeState(POV state)
    {
        //varible for checking if POV exists
        POV temp = activeState;
        activeState = null;
        foreach (POV POV in POVs)
        {
            if(POV == state)
            {
                //update reference variable, then do any setup
                cam.followPoint = POV.transform;
                activeState = POV;
                previousActiveState = temp;
                activeState.activate();
            }
            else
            {
                //Deactivate POVs that shouldn't be active
                POV.deactivate();
            }
        }
        //if no POV found, revert back
        if (activeState == null) 
        {
            cam.followPoint = temp.transform;
            activeState = temp;
            activeState.activate();
            Debug.LogError("POV " + state.gameObject.name + " does not exist in scene. Reverting...");
        }
    }
}
