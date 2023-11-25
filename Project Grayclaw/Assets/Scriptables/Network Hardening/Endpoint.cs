using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

enum Vulnerability
{
    None,
    OOD,
    Malware,
    NP
}

/// <summary>
/// Base script which represents a device that is connected to the network and needs to be fixed.
/// </summary>
/// Require computer script.
public class Endpoint : MonoBehaviour
{
    [SerializeField]
    private Vulnerability vulnerability;
    private void Awake()
    {
        //Fixed by default
        vulnerability = Vulnerability.None;
    }
    
    public void generateVulnerability()
    {
        int randVal = Random.Range((int)Vulnerability.OOD, (int)Vulnerability.NP + 1);//Must update this range every time you add a Vulnerability.
        vulnerability = (Vulnerability)randVal;
        Debug.Log("Gave " + gameObject.name + " vulnerability " + vulnerability.ToString());
    }
    public void fix()
    {
        GameManager.Instance.removeBrokenEndpoint(this);
    }
}
