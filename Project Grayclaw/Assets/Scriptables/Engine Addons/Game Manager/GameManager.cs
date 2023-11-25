using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private PlayerLevelStats stats;

    private List<Endpoint> endpoints;
    private List<Endpoint> brokenEndpoints;
    private void Start()
    {
        endpoints = new List<Endpoint>(FindObjectsOfType<Endpoint>());
        brokenEndpoints = new List<Endpoint>();
        generateBrokenEndpoints(stats.BrokenEndpointNumber);
    }
    public void addEndpoint(Endpoint endpoint)
    {
        endpoints.Add(endpoint);
    }
    public void generateBrokenEndpoints(int number)
    {
        if(endpoints.Count < number)
        {
            Debug.LogError("Cannot generate more unfixed endpoints than there are in scene. Aborting...");
            return;
        }
        List<Endpoint> endpointsCopy = new List<Endpoint>(endpoints);
        for (int i = 0; i < number; i++)
        {
            int randomIndex = (int)Random.Range(0, endpointsCopy.Count);
            endpointsCopy[randomIndex].generateVulnerability();
            brokenEndpoints.Add(endpointsCopy[randomIndex]);
            endpointsCopy.Remove(endpointsCopy[randomIndex]);
        }
    }
    public void removeBrokenEndpoint(Endpoint endpoint)
    {
        endpoints.Remove(endpoint);
        if(brokenEndpoints.Count <= 0) 
        {
            Debug.Log("you won");
        }
    }
}
