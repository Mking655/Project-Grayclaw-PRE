using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// Script that sets up all the nececary gaemplay data for a individual room. 
/// </summary>
[RequireComponent(typeof(Volume), typeof(AudioReverbZone), typeof(mapInfo))]
public class Room : MonoBehaviour
{
    [Tooltip("All physical endpoints in the area of the room. Each row corresponds to the system of the endpoints belong to.")]
    public List<List<physicalEndpoint>> Endpoints = new List<List<physicalEndpoint>>();
    public Volume volume;
    public AudioReverbZone reverbZone;
    public mapInfo info;
    void Awake()
    {
        //inject room refrence into endpoints
        foreach(List<physicalEndpoint> system in Endpoints)
        {
            foreach(physicalEndpoint endpoint in system) 
            {
                endpoint.room = this;
            }
        }   
        //Check for components
        volume = GetComponent<Volume>();
        if(volume == null ) 
        {
            Debug.LogError(gameObject.name + " missing component: Volume");
        }
        reverbZone = GetComponent<AudioReverbZone>();
        if(reverbZone == null)
        {
            Debug.LogError(gameObject.name + " missing component: Reverb Zone");
        }
        info = GetComponent<mapInfo>();
        if(info == null)
        {
            Debug.LogError(gameObject.name + " missing component: Map Info");
        }
    }
}
