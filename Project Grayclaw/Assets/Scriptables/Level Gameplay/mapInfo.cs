using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// A class that contains information about a gameobject for minimap script to use. Used in gathering data, and also when creating a minimap object.
/// </summary>
public class mapInfo : MonoBehaviour
{
    [Header("Apperance")]
    [Tooltip("Check true if this object will dynmically change over time")]
    public bool dynamic;
    public Sprite image;
    public string title;
    [Tooltip("Have the map info automatically be discovered if player enters colider")]
    public bool useCollisionDiscovery = false;
    public bool discovered = false;
    [HideInInspector]
    public map levelmap; //injected on compilation
    [HideInInspector]
    public GameObject mapInstance = null; //the representation on the map. Injected refrence upon map compilation

    [Header("Prefab Varibles")]
    public TMP_Text text;

    private void Start()
    {
        //If dicovered flag checked, discover object
        if (discovered && mapInstance != null)
        {
            discover();
        }
    }
    //If using colision discovery, discover object once player enters collider
    private void OnTriggerEnter(Collider other)
    {
        if (useCollisionDiscovery && other.gameObject.tag == "Player" && discovered == false)
        {
            discover();
        }
    }
    public void discover()
    {
        discovered = true;
        levelmap.revealObject(this as mapInfo);
    }
}
