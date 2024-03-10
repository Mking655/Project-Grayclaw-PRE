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
    public Sprite image;
    public string title;
    public bool discovered = false;
    [HideInInspector]
    public map levelmap; //injected on compilation
    [HideInInspector]
    public GameObject mapInstance = null; //the representation on the map. Injected refrence upon map compilation

    [Header("Prefab Varibles")]
    public TMP_Text text;

    public void discover()
    {
        discovered = true;
        levelmap.revealObject(this as mapInfo);
    }
}
