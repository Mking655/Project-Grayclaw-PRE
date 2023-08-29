using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// a gamemode/state the player can be in
/// </summary>
public abstract class GameState : MonoBehaviour
{
    private GameStateManager manager;
    public GameStateManager getManager()
    {
        return manager;
    }
    private void Start()
    {
        //Only one GameStateManager is allowed per scene using this method
        manager = FindObjectOfType<GameStateManager>();
        if (manager == null)
        {
            Debug.LogError("GameState in scene with no GameState Manager. Please add a manager to the scene.");
        }
    }
    [SerializeField]
    private UnityEvent activateEvent;
    [SerializeField]
    private UnityEvent deactivateEvent;
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
