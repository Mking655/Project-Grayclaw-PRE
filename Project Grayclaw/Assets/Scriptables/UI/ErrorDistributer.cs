using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Populates a scroll view rect with a list of STIGError representations from the given template. Once populated, the representations can be selected to update the "selectedError" variable.
/// </summary>
public class ErrorDistributer : MonoBehaviour
{
    [SerializeField]
    private GameObject CATIRep;
    [SerializeField]
    private GameObject CATIIRep;
    [SerializeField]
    private GameObject CATIIIRep;
    [SerializeField]
    private Endpoint endPoint;
    public List<STIG.STIGerror> errors;
    /// <summary>
    /// When one of the error buttons is selected, in index of the error button relative to the other ones is passed into the Endpoint's selectedErrorIndex variable. It is critical that the order of the error buttons matches that of the slectedErrorList. 
    /// </summary>
    /// <param name="self">Reference to the GameObject calling this method. This should always be an error button.</param>
    public void onErrorSelected(GameObject self)
    {
        endPoint.selectedErrorIndex = self.transform.GetSiblingIndex();
    }
    public void removeAllChildren()
    {
        // Loop through all child transforms of the current GameObject
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            // Destroy the child GameObject
            Destroy(child.gameObject);
        }
    }
    public void populateList()
    {
        removeAllChildren();
        errors = endPoint.SelectedErrorList;
        foreach (STIG.STIGerror error in errors)
        {
            GameObject errorRep;
            //Switch based on severity (but switch statements are ass so im using if)
            if(error.GetSeverity() == severity.CATI)
            {
                // Instantiate the child object as a child of the parent object, set object name to error name
                errorRep = Instantiate(CATIRep, gameObject.transform);
                errorRep.name = error.getName();
                TMP_Text UIName = errorRep.transform.Find("name").GetComponent<TMP_Text>();
                if (UIName != null)
                {
                    UIName.text = error.getName();
                }
                else
                {
                    Debug.LogError("Could not find name gameObject in template hierarchy. Please ensure that there it is named 'name' and has a TMP_Text component");
                }
            }
            if (error.GetSeverity() == severity.CATII)
            {
                // Instantiate the child object as a child of the parent object, set object name to error name
                errorRep = Instantiate(CATIIRep, gameObject.transform);
                errorRep.name = error.getName();
                TMP_Text UIName = errorRep.transform.Find("name").GetComponent<TMP_Text>();
                if (UIName != null)
                {
                    UIName.text = error.getName();
                }
                else
                {
                    Debug.LogError("Could not find name gameObject in template hierarchy. Please ensure that there it is named 'name' and has a TMP_Text component");
                }

            }
            if (error.GetSeverity() == severity.CATIII)
            {
                // Instantiate the child object as a child of the parent object, set object name to error name
                errorRep = Instantiate(CATIIIRep, gameObject.transform);
                errorRep.name = error.getName();
                TMP_Text UIName = errorRep.transform.Find("name").GetComponent<TMP_Text>();
                if (UIName != null)
                {
                    UIName.text = error.getName();
                }
                else
                {
                    Debug.LogError("Could not find name gameObject in template hierarchy. Please ensure that there it is named 'name' and has a TMP_Text component");
                }
            }
        }
    }
}
