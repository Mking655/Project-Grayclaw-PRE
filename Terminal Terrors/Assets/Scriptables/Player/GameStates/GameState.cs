using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class GameState : MonoBehaviour
{
    [SerializeField]
    GAMESTATEID ID;
    public GAMESTATEID getID()
    {
        return ID;
    }
    /// <summary>
    /// When the player is in this gameState, this function will be called every frame by the local scene's GameStateManager.
    /// </summary>
    public void StateUpdate() { }
    /// <summary>
    /// called when player leaves this gameState
    /// </summary>
    public void deactivate(){ }
    /// <summary>
    /// called when player enters this gameState
    /// </summary>
    public void activate(){ }
}
