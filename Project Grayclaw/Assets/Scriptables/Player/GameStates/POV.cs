using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// a gamemode/state the player can be in
/// </summary>
public class POV: MonoBehaviour
{
    private POVManager manager;
    public POVManager getManager()
    {
        return manager;
    }
    private void Awake()
    {
        //Only one GameStateManager is allowed per scene using this method
        manager = POVManager.Instance;
        manager.POVs.Add(this);
    }
    [SerializeField]
    private UnityEvent activateEvent;
    [SerializeField]
    private UnityEvent deactivateEvent;
    /// <summary>
    /// When the player is in this gameState, this function will be called every frame by the local scene's GameStateManager.
    /// </summary>

    public void deactivate()
    { 
        deactivateEvent.Invoke(); 
    }
    /// <summary>
    /// called when player enters this gameState
    /// </summary>
    public void activate()
    { 
        activateEvent.Invoke(); 
    }
}
