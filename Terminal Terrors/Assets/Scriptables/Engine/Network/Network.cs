using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Network")]
public class Network : ScriptableObject
{
    [SerializeField]
    public string netName;
    public List<Endpoint> endPoints = new List<Endpoint>();

    public void addEndpoint(Endpoint endPoint)
    {
        endPoints.Add(endPoint);
    }
    public void removeEndpoint(Endpoint endPoint) 
    {
        endPoints.Remove(endPoint);
    }
    public void print()
    {
        Debug.Log("All clients on " + name + " now include: ");
        foreach (Endpoint endpoint in endPoints)
        {
            Debug.Log(endpoint.endpointName + ", " + endpoint.endpointIP + "; ");
        }
        Debug.Log("----");
    }
    /*
    public void purge()
    {

        List<Endpoint> endpointsCopy = new List<Endpoint>(endPoints); // Create a copy

        foreach (Endpoint endPoint in endpointsCopy)
        {
            endPoint.disconnect();
            //TODO
        }

        Debug.Log("purged " + netName);
        
    }
    */
}
