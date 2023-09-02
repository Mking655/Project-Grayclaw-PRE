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
        connect();
        prompter = GetComponent<Prompter>();
    }
}
