using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic comera follow script
/// </summary>
[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    public Transform followPoint;
    //for unity events
    public void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        gameObject.transform.rotation = followPoint.rotation;
        gameObject.transform.position = followPoint.position;
    }
    
    public void setFOV(float fov)
    {
        gameObject.GetComponent<Camera>().fieldOfView = fov;
    }
}
