using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
/// <summary>
/// a list of all important gameplay locations. Creates a child 2d ui map
/// </summary>
public class map : Singleton<map>
{
    [Header("Data")]
    public MapData data;
    [SerializeField]
    private GameEvent onCompiled;
    [Header("Bounds")]
    [SerializeField]
    [Tooltip("Point on that represents the bottom left boundry of the map, on x and z axis")]
    private Transform MinCorner;
    [SerializeField]
    [Tooltip("Point on that represents the top right boundry of the map, on x and z axis")]
    private Transform MaxCorner;
    [SerializeField]
    [Tooltip("the size of the canvas")]
    private float displaySize;
    private float normalizedXSize = 1;
    private float normalizedZSize = 1;
    private void Awake()
    {
        compileMap();
    }
    /// <summary>
    /// get location on canvas from location on map
    /// </summary>
    public Vector3 normailzeLocation(GameObject obj)
    {
        //TODO
        return new Vector3(0,0,0);
    }
    /// <summary>
    /// recalculate the mapData based on what has been placed in the scene
    /// </summary>
    void compileMap()
    {
        data.Reset();

        //--Endpoint population

        // Find all endpoints in the scene
        Endpoint[] allEndpoints = FindObjectsOfType<Endpoint>();

        // Iterate over each endpoint
        foreach (Endpoint endpoint in allEndpoints)
        {
            // Get the tag of the GameObject to which the endpoint is attached
            string tag = endpoint.gameObject.tag;

            // If the dictionary doesn't contain the tag key, add it
            if (!data.endpoints.ContainsKey(tag))
            {
                data.endpoints[tag] = new List<Endpoint>();
            }

            // Add the endpoint to the correct list based on its tag
            data.endpoints[tag].Add(endpoint);
        }

        //--Setup Canvas

        float baseXSize = MaxCorner.position.x - MinCorner.position.x;
        float baseZSize = MaxCorner.position.z - MinCorner.position.z;
        //normalize canvas scale so that canvas can fit in a square of size displaysize
        if(baseXSize >= baseZSize) 
        {
            normalizedXSize = 1;
            normalizedZSize = baseZSize/baseXSize;
        }
        else if(baseZSize > baseXSize) 
        {
            normalizedZSize = 1;
            normalizedXSize = baseXSize/baseZSize;
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(normalizedXSize * displaySize, normalizedZSize * displaySize);
        Debug.Log(new Vector2(normalizedXSize * displaySize, normalizedZSize * displaySize));

        //--Create 2d representations
        //TODO
        // Iterate over each endpoint
        foreach (Endpoint endpoint in allEndpoints)
        {
            // Assuming vulnerability.correspondingMinigame is now a GameObject prefab
            //GameObject 2d = Instantiate(systemCore.selectedEndpoint.vulnerability.correspondingMinigamePrefab, instancationLocation);

            // Optionally, adjust the instantiated UI's position/scale if necessary
            //minigameInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            //minigameInstance.transform.localScale = Vector3.one; // Ensure scale is reset for UI elements
        }

        onCompiled.TriggerEvent();
    }
    private void OnDestroy()
    {
        data.Reset();
    }
}
