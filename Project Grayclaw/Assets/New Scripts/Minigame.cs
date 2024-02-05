using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minigame : MonoBehaviour
{
    public MinigameManager manager;

    //must call these two functions for it to be a valid minigame
    public void onWin()
    {
        manager.WinMinigame();
    }
    public void onLose()
    {
        manager.LoseMinigame();
    }
}
