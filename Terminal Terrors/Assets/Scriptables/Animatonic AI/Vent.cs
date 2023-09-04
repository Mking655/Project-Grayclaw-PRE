using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
/// <summary>
/// A entrance point for a room that Roxy will check. Every vent should have and acompianing room, and have a trigger that fits the shape of the room.
/// </summary>
public class Vent : MonoBehaviour
{
    //only works if dectcting FPScontroller
    bool isLocked = false;
    [HideInInspector]
    public bool playerInRoom = false;
    [SerializeField]
    private string roomName;
    [SerializeField]
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
    //assuming only one in-game ui manager
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == FPSCamera.playername)
        {
            playerInRoom = true;
            FindAnyObjectByType<InGameUIManager>().room.text = roomName;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == FPSCamera.playername)
        {
            playerInRoom = false;
            FindAnyObjectByType<InGameUIManager>().room.text = "";
        }
    }
}
