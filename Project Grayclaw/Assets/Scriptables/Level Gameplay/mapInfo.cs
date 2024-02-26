using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// A class that contains information about a gameobject for minimap script to use. Used in gathering data, and also when creating a minimap object.
/// </summary>
public class mapInfo : MonoBehaviour
{
    [Header("Apperance")]
    public Image image;
    public string title;
}
