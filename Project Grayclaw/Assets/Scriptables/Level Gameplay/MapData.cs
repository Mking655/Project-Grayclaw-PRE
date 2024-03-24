using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Map Game Data")]
///<summary>
///A scriptable object that contains critical gameplay data about a map.
///</summary>

public class MapData : ScriptableObject
{
    // Dictionary to hold lists of endpoints, keyed by tags
    public Dictionary<string, List<physicalEndpoint>> endpoints = new Dictionary<string, List<physicalEndpoint>>();
    public List<Room> rooms = new List<Room>();
    public void Reset()
    {
        endpoints.Clear();
        rooms = new List<Room>();
    }
}
