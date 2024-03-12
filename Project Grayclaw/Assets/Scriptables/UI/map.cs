using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net;
using static UnityEditor.Progress;

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
    [SerializeField]
    [Tooltip("map item Prefab used for instanciation")]
    mapInfo mapInfoPrefab;
    private float normalizedXSize = 1;
    private float normalizedZSize = 1;
    private List<mapInfo> dynamicItems = new List<mapInfo>();
    private void Awake()
    {
        compileMap();
    }
    private void Update()
    {
        foreach(mapInfo item in dynamicItems)
        {
            updateMapItem(item);
        }
    }
    /// <summary>
    /// recalculate the mapData based on what has been placed in the scene
    /// </summary>
    void compileMap()
    {
        data.Reset();

        //--Setup Canvas

        float baseXSize = MaxCorner.position.x - MinCorner.position.x;
        float baseZSize = MaxCorner.position.z - MinCorner.position.z;
        //normalize canvas scale so that canvas can fit in a square of size displaysize
        if (baseXSize >= baseZSize)
        {
            normalizedXSize = 1;
            normalizedZSize = baseZSize / baseXSize;
        }
        else if (baseZSize > baseXSize)
        {
            normalizedZSize = 1;
            normalizedXSize = baseXSize / baseZSize;
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(normalizedXSize * displaySize, normalizedZSize * displaySize);
        Debug.Log(new Vector2(normalizedXSize * displaySize, normalizedZSize * displaySize));

        //--Endpoint population

        // Find all endpoints in the scene
        Endpoint[] allEndpoints = FindObjectsOfType<Endpoint>();

        // Iterate over each endpoint, adding it to map data and sorting by key
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

        //--Map population (reveals all by default)
        foreach (mapInfo Obj in FindObjectsOfType<mapInfo>())
        {
            //inject this into map refrence for map info
            Obj.levelmap = this;
            createMapItem(Obj);
            if (Obj.dynamic)
            {
                dynamicItems.Add(Obj);
            }
        }

        //--Trigger compilation event
        onCompiled.TriggerEvent();

    }
    /// <summary>
    /// Takes a given mapInfo class and uses it to create a mapInfoPrefab
    /// </summary>
    /// <param name="info"></param>
    public void createMapItem(mapInfo info)
    {
        // Calculate the proportional position relative to Min and Max bounds
        float xProportion = (info.transform.position.x - MinCorner.position.x) / (MaxCorner.position.x - MinCorner.position.x);
        float zProportion = (info.transform.position.z - MinCorner.position.z) / (MaxCorner.position.z - MinCorner.position.z);

        // Adjusting for the normalized canvas size
        Vector2 canvasSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        float xPos = xProportion * canvasSize.x - (canvasSize.x * 0.5f); // Subtract half canvas size to center
        float yPos = zProportion * canvasSize.y - (canvasSize.y * 0.5f); // Use Z for Y because it's a 2D representation

        // Instantiate the map item prefab
        GameObject item = Instantiate(mapInfoPrefab.gameObject, this.transform);

        // Set the instantiated item's anchored position
        RectTransform itemRectTransform = item.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = new Vector2(xPos, yPos);

        // Check for a BoxCollider on the object and scale the map item accordingly
        BoxCollider boxCollider = info.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            // Assume a base scale for your map items (e.g., 10x10 units for no collider)
            Vector2 baseScale = new Vector2(displaySize / 10, displaySize / 10);

            // Adjust scale based on the BoxCollider size (considering only x and z dimensions for a 2D map)
            Vector2 colliderBasedScale = new Vector2(Mathf.Max(baseScale.x, boxCollider.size.x/(MaxCorner.position.x - MinCorner.position.x) * displaySize), Mathf.Max(baseScale.y, boxCollider.size.z / (MaxCorner.position.z - MinCorner.position.z) * displaySize));

            // Apply the larger of the base scale or collider-based scale to ensure visibility
            itemRectTransform.sizeDelta = new Vector2(colliderBasedScale.x, colliderBasedScale.y);
        }
        else
        {
            Vector2 baseScale = new Vector2(displaySize / 10, displaySize / 10);
            // Apply a default scale if no BoxCollider is found
            itemRectTransform.sizeDelta = baseScale; // Default size
        }

        // Additional adjustments for appearance
        item.GetComponent<mapInfo>().text.text = info.title;
        item.GetComponent<Image>().sprite = info.image;
        info.mapInstance = item;
        item.SetActive(false);
    }
    //TODO: move this in to mapInfo script?
    /// <summary>
    /// Updates all map item to map info 
    /// </summary>
    /// <param name="info"></param>
    public void updateMapItem(mapInfo info)
    {
        if(info.dynamic == true)
        {
            // Update position
            // Calculate the proportional position relative to Min and Max bounds
            float xProportion = (info.transform.position.x - MinCorner.position.x) / (MaxCorner.position.x - MinCorner.position.x);
            float zProportion = (info.transform.position.z - MinCorner.position.z) / (MaxCorner.position.z - MinCorner.position.z);

            // Adjusting for the normalized canvas size
            Vector2 canvasSize = gameObject.GetComponent<RectTransform>().sizeDelta;
            float xPos = xProportion * canvasSize.x - (canvasSize.x * 0.5f); // Subtract half canvas size to center
            float yPos = zProportion * canvasSize.y - (canvasSize.y * 0.5f); // Use Z for Y because it's a 2D representation
                                             
            // Set the instantiated item's anchored position
            RectTransform itemRectTransform = info.mapInstance.GetComponent<RectTransform>();
            itemRectTransform.anchoredPosition = new Vector2(xPos, yPos);

            info.mapInstance.GetComponent<mapInfo>().text.text = info.title;
            info.mapInstance.GetComponent<Image>().sprite = info.image;
        }
        else
        {
            return;
        }
    }
    /// <summary>
    /// Reveal and item on the 2d map.
    /// </summary>
    public void revealObject(mapInfo info)
    {
        info.mapInstance.SetActive(true);
        Debug.Log("Discovered: " + info.name);
    }
    private void OnDestroy()
    {
        data.Reset();
    }
}
