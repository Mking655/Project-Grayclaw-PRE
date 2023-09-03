using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ErrorFixer : MonoBehaviour
{
    public UnityEvent onAllErrorsFixed;
    [SerializeField]
    private Endpoint endpoint;
    [SerializeField]
    private ErrorDistributer distributer;
    [SerializeField]
    private ErrorViewer viewer;
    public void fixError()
    {
        //Player must have selected an error
        if(endpoint.selectedErrorIndex != -1)
        {

            //Remove the error
            endpoint.SelectedErrorList.RemoveAt(endpoint.selectedErrorIndex);
            endpoint.selectedErrorIndex = -1;
            //update error list and display
            distributer.populateList();
            viewer.updateDisplay();


            if (endpoint.SelectedErrorList.Count == 0)
            {
                onAllErrorsFixed.Invoke();
            }
        }
    }
}
