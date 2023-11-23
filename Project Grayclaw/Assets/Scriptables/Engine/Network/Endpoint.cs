using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using TMPro;
using static STIG;
using UnityEngine.Events;

public enum STATE
{
    online,
    offline
}
/// <summary>
/// Base script which represents a device that is connected to the network and needs to be fixed.
/// </summary>
public abstract class Endpoint : MonoBehaviour
{
    protected STATE state;
    public Network network;
    public string endpointName;
    public string endpointIP;
    public GameEvent onNetworkChange;
    public List<STIG> STIGS;
    private bool hardened = false;
    public bool getHardened()
    {
        return hardened;
    }
    //2 Dimensional array where first dimension corresponds to STIG type, second dimension corresponds to error
    public List<List<STIG.STIGerror>> STIGErrors;
    public UnityEvent onHardened;
    [SerializeField]
    private TMP_Dropdown STIGSelector;
    [HideInInspector]
    public List<STIG.STIGerror> SelectedErrorList;
    [HideInInspector]
    //-1 means no error selected
    public int selectedErrorIndex = -1;
    /// <summary>
    /// Method that fixes an error
    /// </summary>
    public void removeSelectedSTIGErrors()
    {
        int selectedSTIGIndex = STIGSelector.value;
        //Check if dropdowns and error list are in order
        if (STIGErrors[selectedSTIGIndex] == SelectedErrorList) 
        {
            STIGErrors.Remove(SelectedErrorList);
            STIGS.RemoveAt(selectedSTIGIndex);
            if(STIGErrors.Count == 0)
            {
                markAsHardened();
            }
        }
        else
        {
            Debug.LogWarning("Dropdown order does not match STIGError two dimensional list order");
        }
    }
    public void clearSelectedErrorIndex()
    {
        selectedErrorIndex = -1;
    }
    //Determines if the endpoint should have more or less errors in the set range. In short, it is how poor the computer was set up.
    [SerializeField]
    [Range(0f, 1f)]
    private float ErrorBias;
    public void markAsHardened()
    {
        onHardened.Invoke();
        hardened = true;
        Debug.Log("you have hardened endpoint: " + endpointName);
        //Check if the entire network is hardened
        network.checkIfHardened();
    }
    /// <summary>
    /// Fill each STIG up with a desired range of random errors from that STIG (No repeats)
    /// </summary>
    public void generateErrors()
    {
        //Initialize List
        STIGErrors = new List<List<STIGerror>>();
        int errorRange = (int)(PlayerSettings.maxErrors * ErrorBias);
        for (int i =  0; i < STIGS.Count; ++i)
        {
            STIGErrors.Add(new List<STIGerror>());
            int errorAmount = 1 + Random.Range(0, errorRange);
            Debug.Log(errorAmount + " number of errors (Max: " + errorRange + ") will be added to STIG " + STIGS[i].name + " for " + endpointName);
            //Copy to ensure no repeats
            List<STIGerror> listCopy = new List<STIGerror>(STIGS[i].getErrorList());
            for(int j = 0; j < errorAmount; j++) 
            {
                //Add a randomly selected error from the STIG error list copy, and then remove that error from the copy so it is not selected again
                int randomIndex = Random.Range(0, listCopy.Count);
                Debug.Log("Added error " + listCopy[randomIndex].getName() + " in STIG " + STIGS[i].name + " at endpoint " + endpointName);
                STIGErrors[i].Add(listCopy[randomIndex]);
                listCopy.RemoveAt(randomIndex);
                //If there are no possible errors left, break loop
                if (listCopy.Count == 0)
                {
                    break;
                }
            }
        }
    }
    private void Start()
    {
        generateErrors();
    }
    public virtual void connect()
    {
        network.addEndpoint(this);
        onNetworkChange.TriggerEvent();
        state = STATE.online;
    }
    public virtual void disconnect()
    {
        network.removeEndpoint(this);
        onNetworkChange.TriggerEvent();
        state = STATE.offline;
    }
    //move this out for persistent endpoints?
    private void OnDisable()
    {
        disconnect();
    }
    //assumes not persistent
    private void OnDestroy()
    {
        disconnect();
    }
}
