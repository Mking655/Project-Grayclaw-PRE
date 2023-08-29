using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

public enum STATE
{
    online,
    offline,
    hardened
}
/// <summary>
/// Base script which represents a device that is connected to the network and needs to be fixed.
/// </summary>
public abstract class Endpoint : MonoBehaviour
{
    public string endpointName;
    public string endpointIP;
    public GameEvent onNetworkChange;
    protected STATE state;

    public Network network;
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
    //assumes not persistant
    private void OnDestroy()
    {
        disconnect();
    }
}
