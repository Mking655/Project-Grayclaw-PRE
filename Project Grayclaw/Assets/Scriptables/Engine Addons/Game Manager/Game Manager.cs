using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton
{
    [SerializeField]
    private PlayerLevelStats stats;
    private List<Endpoint> endpoints;
    private List<Endpoint> brokenEndpoints;
    private void Start()
    {
        //clear list on start
        foreach (Endpoint endpoint1 in endpoints)
        {
            endpoints.Remove(endpoint1);
        }
        //clear broken list on start
        foreach (Endpoint endpoint1 in brokenEndpoints)
        {
            brokenEndpoints.Remove(endpoint1);
        }
    }
    public void addEndpoint(Endpoint endpoint)
    {
        endpoints.Add(endpoint);
    }
    public void generateBrokenEndpoints(int number)
    {
        if(endpoints.Count < number)
        {
            Debug.LogError("Cannot generate more broken endpoints than their are in scene.");
            return;
        }
        for(int i = 0; i < number; i++)
        {
            //TODO
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
