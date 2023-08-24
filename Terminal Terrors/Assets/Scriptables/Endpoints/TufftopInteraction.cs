using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Prompter))]
public class TufftopInteraction : MonoBehaviour
{
    //provides funtiality for view PC prompt
    Prompter prompter;
    [SerializeField]
    GameObject Parent;
    private void Awake()
    {
        prompter = GetComponent<Prompter>();
    }
    private void Update()
    {
        if (prompter.inRange && GamemodeManager.currentGameMode != GAMEMODE.tufftop)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                //assuming only one per scene
                FindObjectOfType<GamemodeManager>().changeGameMode(GAMEMODE.tufftop);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pickUp();
            }
        }
    }
    void pickUp()
    {
        GamemodeManager.hasTufftop = true;
        Parent.SetActive(false);
        Debug.Log("player has tuffTop?" + GamemodeManager.hasTufftop);
    }
}
