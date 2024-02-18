using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// An example of a properly implemented fixing minigame. For debugging purposes only.
/// </summary>
public class DebugMinigame : Minigame
{
    public override void Win()
    {
        Debug.Log("you won the game.");
        base.Win();
        Destroy(gameObject);
    }
    public override void Lose()
    {
        Debug.Log("you lost the game.");
        base.Lose();
        Destroy(gameObject);
    }
}
