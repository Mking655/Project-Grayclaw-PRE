using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A gamemode/POV the player can be in
/// </summary>
public class POV: MonoBehaviour
{
    public POVManager manager = POVManager.Instance;

    private void Awake()
    {
        manager.POVs.Add(this);
    }
    [SerializeField]
    private UnityEvent activateEvent;
    [SerializeField]
    private UnityEvent deactivateEvent;
    /// <summary>
    /// Called when player exits this gamePOV.
    /// </summary>
    public void deactivate()
    { 
        deactivateEvent.Invoke(); 
    }
    /// <summary>
    /// Called when player enters this gamePOV. Additional logic outside the unity event will usally be handled in the corresponing script attached to the POV. 
    /// </summary>
    public void activate()
    { 
        activateEvent.Invoke(); 
    }
    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.POVs.Remove(this);
        }
    }

}
