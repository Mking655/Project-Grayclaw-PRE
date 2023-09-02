using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Prompter))]
public class LocalEndpoint : Endpoint
{
    Prompter prompter;
    private void Awake()
    {
        prompter = GetComponent<Prompter>();
        //ensure endpoint is not connected at start of game
        disconnect();
    }
}
