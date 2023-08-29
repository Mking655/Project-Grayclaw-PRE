using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
public class STIGDropdown : MonoBehaviour
{
    [SerializeField]
    private List<STIG> stigs = new List<STIG>();
    public STIG selectedSTIG { get; private set; }
    private TMP_Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
    }
    public void PopulateDropdown()
    {
        // Clear the existing options
        dropdown.ClearOptions();

        // Create a new list of options from the endpoints in the network
        TMP_Dropdown.OptionData[] options = new TMP_Dropdown.OptionData[stigs.Count];

        for (int i = 0; i < stigs.Count; i++)
        {
            options[i] = new TMP_Dropdown.OptionData(stigs[i].name);
        }

        // Set the options for the dropdown
        dropdown.options = new List<TMP_Dropdown.OptionData>(options);
    }
    private void OnDropdownValueChanged(int index)
    {
        selectedSTIG = stigs[index];
    }
}
