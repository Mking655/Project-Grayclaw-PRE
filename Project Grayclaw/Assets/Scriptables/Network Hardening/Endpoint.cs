using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.IO;
using UnityEngine.UIElements;

/// <summary>
/// Base script which represents a device that is connected to the network and needs to be fixed.
/// </summary>
/// Require computer script.
public class Endpoint : MonoBehaviour
{
    public ErrorList errorList;
    public Transform errorSpawnPoint;
    private Error error;
    public void generateError()
    {
        int randVal = Random.Range(0, errorList.possibleTypes.Count);   
        error = Instantiate(errorList.getErrors()[errorList.possibleTypes[randVal]], errorSpawnPoint);
        Debug.Log("Given error of type: " + errorList.possibleTypes[randVal] + " to gameobject:" + gameObject.name);
    }
    public void fix()
    {
        error.Fix();
        GameManager.Instance.removeBrokenEndpoint(this);
    }
}
