using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Prompter))]
public class RemoteEndpoint : Endpoint
{
    //dont worry about text implementation
    Prompter prompter;
    private void Awake()
    {
        prompter = GetComponent<Prompter>();
        //ensure endpoint is not connected at start of game
        disconnect();
    }

    private void Update()
    {
        //turn computer on or off
        if(Input.GetKeyDown(KeyCode.E) && prompter.inRange) 
        {
            if(state == STATE.online)
            {
                disconnect();
            }
            else if(state == STATE.offline)
            {
                connect();
            }
        }
    }
    //implementation of prompter
    public override void connect()
    {
        prompter.text = "disconnect from network";
        network.addEndpoint(this);
        onNetworkChange.TriggerEvent();
        state = STATE.online;
        network.print();
    }

    public override void disconnect()
    {
        prompter.text = "connect to network";
        network.removeEndpoint(this);
        onNetworkChange.TriggerEvent();
        state = STATE.offline;
    }
}
