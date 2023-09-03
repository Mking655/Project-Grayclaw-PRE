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
}
