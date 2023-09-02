using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Network")]
public class Network : ScriptableObject
{
    [SerializeField]
    public string netName;
    [SerializeField]
    private GameEvent onNetworkHardened;
    [HideInInspector]
    public List<Endpoint> endPoints = new List<Endpoint>();
    //Ths implementation of a win state assumes only one network
    public void checkIfHardened()
    {
        foreach (Endpoint ep in endPoints) 
        { 
            if(ep.getHardened() == false) 
            {
                Debug.Log("you have yet to harden: " + ep.endpointName);
                return;
            }
        }
        Debug.Log("Network Hardened. You win.");
        onNetworkHardened.TriggerEvent();
    }
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
