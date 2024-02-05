using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Prompter : MonoBehaviour
{
    [SerializeField]
    private UnityEvent interaction;
    //only works if detecting FPScontroller
    //Updates the reticle to represent relevant action
    public bool inRange;
    public string text;
    InGameUIManager gameUIManager;
    private void Awake()
    {
        //ASSUMES ONLY ONE PER SCENE
        gameUIManager = FindAnyObjectByType<InGameUIManager>();
    }
    private void Update()
    {
        if (inRange)
        {
            gameUIManager.updateReticle(text);
            if(Input.GetKeyDown(KeyCode.E))
            {
                interaction.Invoke();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            gameUIManager.resetReticle();
        }
    }
    private void OnDisable()
    {
        gameUIManager.resetReticle();
    }
}
