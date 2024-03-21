using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class POVManager : Singleton<POVManager>
{
    [SerializeField]
    private POV startingPOV;//choose what POV player should be in
    [SerializeField]
    public PlayerCamera cam;//define in inspector
    [HideInInspector]
    public List<POV> POVs = new List<POV>();
    [HideInInspector]
    public POV previousActivePOV;
    private POV activePOV;
    void Awake()
    {
        POVs = new List<POV>(FindObjectsByType<POV>(FindObjectsSortMode.None));
        previousActivePOV = null;
        if (startingPOV == null)
        {
            Debug.LogWarning("No starting POV for game. Defaulting to 0,0,0");
            cam.followPoint = cam.transform;
            activePOV = null;
            return;
        }
        foreach (POV p in POVs)
        {
            p.deactivate();
        }
        changePOV(startingPOV);
    }
    public POV getActivePOV()
    {
        return activePOV;
    }
    public void revertPOV()
    {
        if(previousActivePOV != null)
        {
            previousActivePOV.activate();
            activePOV.deactivate();
            cam.followPoint = previousActivePOV.transform;
            activePOV = previousActivePOV;
            previousActivePOV = null;
        }
        else
        {
            Debug.Log("no previous POV has been logged.");
        }
    }
    /// <summary>
    /// Changes the player's POV(gamemode) by enumerating through all given POVs.
    /// </summary>
    public void changePOV(POV newPOV)
    {
        if (!POVs.Contains(newPOV))
        {
            Debug.LogError($"POV {newPOV.gameObject.name} does not exist in scene. Reverting...");
            return;
        }

        if (activePOV != null)
        {
            activePOV.deactivate();
        }

        previousActivePOV = activePOV;
        activePOV = newPOV;

        cam.followPoint = newPOV.transform;
        activePOV.activate();
        Debug.Log(activePOV.name + " changed to.");
    }

}
