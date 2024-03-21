using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// Script that sets up all the nececary gaemplay data for a individual room. 
/// </summary>
[RequireComponent(typeof(AudioReverbZone), typeof(mapInfo))]
public class Room : MonoBehaviour
{
    [Tooltip("All physical endpoints in the area of the room.")]
    public List<physicalEndpoint> Endpoints = new List<physicalEndpoint>();
    [HideInInspector]
    public AudioReverbZone reverbZone;
    [HideInInspector]
    public mapInfo info;
    void Awake()
    {
        //inject room refrence into endpoints
        foreach(physicalEndpoint endpoint in Endpoints) 
        {
            endpoint.room = this;
        }
        //Check for components
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
