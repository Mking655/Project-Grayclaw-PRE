using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinigameManager))]
public class SystemCore : MonoBehaviour
{
    public List<Endpoint> endpoints;
    public List<vulnerability> vulnerabilities; //Define all possible vulnerabilities the system can have in inspector
    [HideInInspector]
    public Endpoint selectedEndpoint = null;

    void Start()
    {
        //initialize the endpoints by giving them a random vulnerability from the defined list
        foreach (Endpoint ep in endpoints)
        {
            int randomIndex = (int)UnityEngine.Random.Range(0, vulnerabilities.Count);
            if (vulnerabilities[randomIndex].correspondingMinigamePrefab.GetComponent<Minigame>() == null)
            {
                Debug.LogError("Invalid vulnerability: Defined vulnerability without a minigame component.");
            }
            else
            {
                ep.vulnerability = vulnerabilities[randomIndex];
                Debug.Log("Given vulnerability: " + vulnerabilities[randomIndex].correspondingMinigamePrefab.name + " to: " + ep.gameObject.name);
            }
        }
    } 
    public void selectEndpoint(Endpoint endpoint)
    {
        if(endpoints.Contains(endpoint))
        {
            selectedEndpoint = endpoint;
        }
        else
        {
            Debug.LogError("Cannot select an endpoint that doesn't have a core");
        }
    }
    // Method to activate the core computer UI
    //TODO
    public void ActivateCoreComputer()
    {
        // Logic to display endpoints on the UI map
    }

    // Method to start fixing an endpoint
    //TODO
    public void StartFixMinigame(Endpoint endpoint)
    {
        // Activate the fixing minigame UI
        // Based on the minigame result, call endpoint.ChangeState appropriately
        gameObject.GetComponent<MinigameManager>().StartMinigame();
    }
}
