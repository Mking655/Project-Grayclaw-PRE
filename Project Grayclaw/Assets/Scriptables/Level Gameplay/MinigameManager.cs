using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A manager for a fixing minigame that handles the instanciates a minigame prefab and handles a win or a loss.
/// </summary>
[RequireComponent(typeof(SystemCore))]
public class MinigameManager : MonoBehaviour
{
    private Minigame activeMinigame;
    private SystemCore systemCore;
    public RectTransform instancationLocation; //where the manager should create the minigames
    private void Start()
    {
        systemCore = gameObject.GetComponent<SystemCore>();
    }
    /// <summary>
    /// Starts minigame for selected endpoint
    /// </summary>
    public void StartMinigame()
    {
        // Logic to start the minigame based on endpoint's vulnerability
        if (systemCore.selectedEndpoint == null)
        {
            Debug.Log("Cannot start a fixing minigame if no endpoint is selected.");
            return;
        }
        else if (systemCore.selectedEndpoint.vulnerability == null || systemCore.selectedEndpoint.state == EndpointState.Broken)
        {
            Debug.Log("Cannot start a fixing minigame if the endpoint has no vulnerability(fixed) or is broken.");
            return;
        }
        if (activeMinigame != null)
        {
            Destroy(activeMinigame.gameObject); // Ensure only one minigame is active at a time
        }

        // Assuming vulnerability.correspondingMinigame is now a GameObject prefab
        GameObject minigameInstance = Instantiate(systemCore.selectedEndpoint.vulnerability.correspondingMinigamePrefab, instancationLocation);

        activeMinigame = minigameInstance.GetComponent<Minigame>();
        activeMinigame.manager = this; // Inject reference

        // Optionally, adjust the instantiated UI's position/scale if necessary
        minigameInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        minigameInstance.transform.localScale = Vector3.one; // Ensure scale is reset for UI elements

    }

    public void WinMinigame()
    {
        systemCore.selectedEndpoint.ChangeState(EndpointState.Fixed);
        // Additional win logic
    }

    public void LoseMinigame()
    {
        // Logic for losing the minigame
        systemCore.selectedEndpoint.ChangeState(EndpointState.Vulnerable);
    }

}
