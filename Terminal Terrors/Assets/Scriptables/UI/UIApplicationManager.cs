using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that helps manage windows in a computer UI
/// </summary>
public class UIApplicationManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> UIApplications = new List<GameObject>();

    /// <summary>
    /// Deactivates everthing but the desired application/window,then activates that desired application.
    /// </summary>
    /// <param name="window"> the desired application/window you want to appear</param>
    public void changeApplication(GameObject window) 
    { 
        foreach(GameObject app in UIApplications)
        {
            if(app != window)
            {
                app.SetActive(false);
            }
            if(app == window)
            {
                app.SetActive(true);
                return;
            }
        }
        //if code reaches this point, that means the desired window was not found
        Debug.LogError("Desired window not found.");
    }
}
