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

    void Update()
    {
        gameObject.transform.rotation = followPoint.rotation;
        gameObject.transform.position = followPoint.position;
    }
}
