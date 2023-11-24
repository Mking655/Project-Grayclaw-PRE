using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Base script which represents a device that is connected to the network and needs to be fixed.
/// </summary>
public class Endpoint : MonoBehaviour
{
    //Script has a random chance of needing to be fixed. If broken, add to level stats.
    //Diffrent types of errors, all requireing diffrent tasks to fix
    //Once fixed, need to send a signal back to level stats.
}
