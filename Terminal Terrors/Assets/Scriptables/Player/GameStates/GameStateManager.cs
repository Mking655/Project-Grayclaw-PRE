using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMESTATEID
{
    Walking,
    Endpoint,
    Firewall,
    Dead
}
public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private List<GameState> gameStates = new List<GameState>();
    private GameState activeState;
    GameState getActiveState()
    {
        return activeState;
    }
    /// <summary>
    /// Changes the player's gameState(gamemode) by enumerating through all given gamestates.
    /// </summary>
    /// <param name="gAMESTATEID">The intended game state.</param>
    public void changeState(GAMESTATEID gAMESTATEID)
    {
        //varible for chcking if gameState exists
        GameState temp = activeState;
        activeState = null;
        foreach (GameState gameState in gameStates)
        {
            if(gameState.getID() == gAMESTATEID)
            {
                //update refrence varible, then do any setup
                activeState = gameState;
                activeState.activate();
            }
            else
            {
                //Deactivate gameStates that shouldn't be active
                gameState.deactivate();
            }
        }
        //if no gameState found, revert back
        if (activeState == null) 
        {
            activeState = temp;
            activeState.activate();
            Debug.LogError("GameState " + gAMESTATEID + " does not exist in scene. Reverting...");
        }
    }
}
