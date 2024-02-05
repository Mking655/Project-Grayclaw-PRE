using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An error that coresponds to an endpoint. Manages fixing procedure.
/// </summary>
//Will be attached to a prefab that is instanciated from an endpoint script.

public enum ERROR
{
    OutOfDate,
    Malware,
    NeuralPresence
}
[RequireComponent(typeof(RectTransform))]
public abstract class Error : MonoBehaviour
{
    [Header("This has to be defined in the inpsector for some retarded reason")]
    [SerializeField]
    protected ERROR errorKey;

    public ERROR getErrorKey()
    {
        return errorKey;
    }

    /// <summary>
    /// Fixes and removes the error.
    /// </summary>
    public abstract void Fix();
    //Game logic
}
