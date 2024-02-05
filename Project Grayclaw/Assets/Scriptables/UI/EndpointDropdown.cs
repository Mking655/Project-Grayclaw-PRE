using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(GameEventListener))]

public class EndpointDropdown : MonoBehaviour
{
    /* TODO: POSSIBLY modify to be a general dynamic options display
    //modified from Chat GPT 3.5
    TMP_Dropdown dropdown;
    public Network network; // Reference to your Network scriptable object

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        // Add a listener for when the dropdown value changes
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Populate the dropdown initially
        PopulateDropdown();
    }

    public void PopulateDropdown()
    {
        // Clear the existing options
        dropdown.ClearOptions();

        // Create a new list of options from the endpoints in the network
        TMP_Dropdown.OptionData[] options = new TMP_Dropdown.OptionData[network.endPoints.Count];

        for (int i = 0; i < network.endPoints.Count; i++)
        {
            //Images possibly?
            options[i] = new TMP_Dropdown.OptionData(network.endPoints[i].endpointName);
        }

        // Set the options for the dropdown
        dropdown.options = new List<TMP_Dropdown.OptionData>(options);
    }
    //TODO
    private void OnDropdownValueChanged(int index)
    {
        // This method will be called when the dropdown value changes
        // Use the index to retrieve the selected endpoint and do something with it
        // For example: Endpoint selectedEndpoint = network.endpoints[index];
    }
    */
}
