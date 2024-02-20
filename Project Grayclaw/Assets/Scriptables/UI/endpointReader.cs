using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// A bare-bones script that updates text so that it displays in the UI. Update has to be manually called.
/// </summary>
public class endpointReader : MonoBehaviour
{
    public SystemCore core;
    public TMP_Text title;
    public TMP_Text status;
    public TMP_Text Descripion;
    public Button startbutton;
    private void Awake()
    {
        updateDisplay();
    }
    public void updateDisplay()
    {
        if(core.selectedEndpoint == null)
        {
            title.text = string.Empty;
            status.text = string.Empty;
            Descripion.text = "-Please click on an endpoint to see its status-";
            return;
        }
        title.text = core.selectedEndpoint.endpointName;
        switch(core.selectedEndpoint.state) 
        {
            case EndpointState.Fixed:
                status.text = "FIXED";
                Descripion.text = core.selectedEndpoint.criticalFunction;
                startbutton.gameObject.SetActive(false);
                break;
            case EndpointState.Vulnerable:
                status.text = "VULNERABLE";
                Descripion.text = core.selectedEndpoint.criticalFunction + ". Has Vulnerability " + core.selectedEndpoint.vulnerability.name;
                startbutton.gameObject.SetActive(true);
                break;
            case EndpointState.Broken:
                status.text = "VULNERABLE";
                Descripion.text = core.selectedEndpoint.criticalFunction + ". Broken, needs to be repaired on site.";
                startbutton.gameObject.SetActive(false);
                break;
        }
    }
}
