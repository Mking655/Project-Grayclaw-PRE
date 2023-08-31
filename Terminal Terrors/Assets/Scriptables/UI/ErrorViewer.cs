using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;

/// <summary>
/// Displays all the parts of a STIGError in UI format.
/// </summary>
public class ErrorViewer : MonoBehaviour
{
    [SerializeField]
    private Endpoint endpoint;
    [SerializeField]
    private TMP_Text errorNameText;
    [SerializeField]
    private TMP_Text errorSeverityText;
    [SerializeField]
    private TMP_Text errorDescriptionText;

    private void Start()
    {
        updateDisplay();
    }
    /// <summary>
    /// Updates the display to the endpoint's currently selected error
    /// </summary>
    public void updateDisplay()
    {
        //if no error is selected, clear text
        if (endpoint.selectedErrorIndex == -1)
        {
            errorNameText.text = "No vulnerability Selected";
            errorSeverityText.text = "";
            errorDescriptionText.text = "";
        }
        if (errorNameText != null && errorSeverityText != null && errorDescriptionText != null) 
        {
            errorNameText.text = endpoint.SelectedErrorList[endpoint.selectedErrorIndex].getName();
            errorSeverityText.text = endpoint.SelectedErrorList[endpoint.selectedErrorIndex].GetSeverityName();
            errorDescriptionText.text = endpoint.SelectedErrorList[endpoint.selectedErrorIndex].getDescription();
        }
        else
        {
            Debug.LogError("Not all fields filled out for error viewer");
        }
    }
}
