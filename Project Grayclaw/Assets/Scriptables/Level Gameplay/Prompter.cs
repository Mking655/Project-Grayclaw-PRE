using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Prompter : MonoBehaviour
{
    [SerializeField]
    private UnityEvent interaction;
    [SerializeField]
    private UnityEvent exit;
    //only works if detecting FPScontroller
    //Updates the reticle to represent relevant action
    public bool inRange;
    public string text;
    public GameObject visualCue;
    private void Start()
    {
        visualCue.SetActive(false);
    }
    private void Update()
    {
        if (inRange)
        {
            if (Input.GetAxis("Interact") == -1)
            {
                visualCue.SetActive(true);
                exit.Invoke();
            }
            if (Input.GetAxis("Interact") == 1)
            {
                visualCue.SetActive(false);
                interaction.Invoke();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
            visualCue.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            visualCue.SetActive(false);
        }
    }
    private void OnDisable()
    {
        visualCue.SetActive(false);
    }
}
