using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;

public class Tufftop : MonoBehaviour
{
    public Network network;

    //just for the exit button
    public void exit()
    {
        //assuming only one per scene
        FindObjectOfType<GamemodeManager>().changeGameMode(GAMEMODE.fps);
    }
}
