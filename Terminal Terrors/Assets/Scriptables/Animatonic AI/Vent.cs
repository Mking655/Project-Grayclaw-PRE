using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    bool isLocked = false;
    public void setLocked(bool locked)
    {
        isLocked = locked;
    }
    /// <summary>
    /// Checks if the player is there.
    /// </summary>
    public void check()
    {
        Debug.Log("roxy checked vent: " + gameObject.name);
    }
}
