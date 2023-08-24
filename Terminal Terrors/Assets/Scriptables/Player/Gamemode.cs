using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GameEventListener))]
//need this for handling diffrent types of gamemode changes
public class Gamemode : MonoBehaviour
{
    public GAMEMODE gamemode;
    public UnityEvent enableAction;
    public UnityEvent disableAction;
    //put this function and this function only into your onGamemodeChange event listener on your player controller
    public void changeGamemode()
    {
        if(gamemode == GamemodeManager.currentGameMode)
        {
            //Debug.Log(gameObject.name + " is enabling");
            enableAction.Invoke();
        }
        else
        {
            //Debug.Log(gameObject.name + " is disabling");
            disableAction.Invoke();
        }
    }
}
