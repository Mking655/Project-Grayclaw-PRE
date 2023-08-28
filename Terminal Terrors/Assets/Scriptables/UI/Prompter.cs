using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Prompter : MonoBehaviour
{
    [SerializeField]
    private UnityEvent interaction;
    //only works if dectcting FPScontroller
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
        if (other.gameObject.name == FPSCamera.playername)
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == FPSCamera.playername)
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
