using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private Minigame minigame;
    private SystemCore systemCore;
    private void Start()
    {
        systemCore = gameObject.GetComponent<SystemCore>();
    }
    public void StartMinigame()
    {
        // Logic to start the minigame based on endpoint's vulnerability
        // eventully change this so it instanciates a prefab
        minigame = systemCore.selectedEndpoint.vulnerability.correspondingMinigame;
    }

    public void WinMinigame()
    {
        systemCore.selectedEndpoint.ChangeState(EndpointState.Fixed);
        // Additional win logic
    }

    public void LoseMinigame()
    {
        // Logic for losing the minigame
    }

}
