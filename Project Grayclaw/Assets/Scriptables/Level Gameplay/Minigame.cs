using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Abstract class for all minigames. These child classes will handle the logic of the minigames and will define win states and loss states.
/// </summary>
public abstract class Minigame : MonoBehaviour
{
    [HideInInspector]
    public MinigameManager manager; //This will be injected by the minigame manager when the Minigame is instanciated

    //must call these two functions for it to be a valid minigame
    public virtual void Win()
    {
        manager.WinMinigame();
    }
    public virtual void Lose()
    {
        manager.LoseMinigame();
    }
}
