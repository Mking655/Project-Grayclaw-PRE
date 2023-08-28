using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameState : MonoBehaviour
{
    [SerializeField]
    UnityEvent activateEvent;
    [SerializeField]
    UnityEvent deactivateEvent;
    [SerializeField]
    GAMESTATEID ID;
    public GAMESTATEID getID()
    {
        return ID;
    }
    /// <summary>
    /// When the player is in this gameState, this function will be called every frame by the local scene's GameStateManager.
    /// </summary>
    public virtual void StateUpdate() { }
    /// <summary>
    /// called when player leaves this gameState
    /// </summary>
    public virtual void deactivate()
    { deactivateEvent.Invoke(); }
    /// <summary>
    /// called when player enters this gameState
    /// </summary>
    public virtual void activate()
    { activateEvent.Invoke(); }
}
