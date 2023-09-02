using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent onEventTriggered;
    /// <summary>
    /// Listen when inactive?
    /// </summary>
    public bool persistent = false;
    public void OnEventTriggered()
    {
        onEventTriggered.Invoke();
    }

    // code for persistent listeners
    private void Awake()
    {
        if (persistent == true)
        {
            gameEvent.AddListener(this);
        }
    }

    private void OnDestroy()
    {
        if (persistent == true) 
        {
            gameEvent.RemoveListener(this);
        }
    }

    // code for non persistent listeners
    void OnEnable()
    {
        if(persistent == false)
        {
            gameEvent.AddListener(this);
        }
    }

    void OnDisable()
    {
        if(persistent == false)
        {
            gameEvent.RemoveListener(this);
        }
    }
}
