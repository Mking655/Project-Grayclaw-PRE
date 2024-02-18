using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO persistant singleton
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static T Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        if(Instance != this)
        {
            Debug.LogWarning(gameObject.name + " is an illegal singleton instance. Removing singleton component...");
            Destroy(this);
        }
    }
    //reset instance so instance is not persistent across scenes
    private void OnDestroy()
    {
        Instance = null;
    }
}
