using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Populates a dropdown with the STIGS of a given endpoint
/// </summary>
[RequireComponent(typeof(TMP_Dropdown))]
public class STIGDropdown : MonoBehaviour
{
    [SerializeField]
    private Endpoint endpoint;
    public STIG selectedSTIG { get; private set; }
    private TMP_Dropdown dropdown;
    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
    }
    public void PopulateDropdown()
    {
        // Clear the existing options
        dropdown.ClearOptions();

        // Create a new list of options from the endpoints in the network
        TMP_Dropdown.OptionData[] options = new TMP_Dropdown.OptionData[endpoint.STIGS.Count];

        for (int i = 0; i < endpoint.STIGS.Count; i++)
        {
            options[i] = new TMP_Dropdown.OptionData(endpoint.STIGS[i].name);
        }

        // Set the options for the dropdown
        dropdown.options = new List<TMP_Dropdown.OptionData>(options);
    }
    public void OnDropdownValueChanged()
    {
        endpoint.SelectedErrorList = endpoint.STIGErrors[gameObject.GetComponent<TMP_Dropdown>().value];
        Debug.Log(endpoint.SelectedErrorList.Count + " errors detected in STIG index " + gameObject.GetComponent<TMP_Dropdown>().value + " for " + endpoint.endpointName);
    }
}
