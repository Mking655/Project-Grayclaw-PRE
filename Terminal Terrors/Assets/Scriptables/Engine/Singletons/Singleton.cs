using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO persistant singleton
public class Singleton : MonoBehaviour
{
    //Code taken from: https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static Singleton Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    //reset instance so instance is not persistent accross scenes
    private void OnDestroy()
    {
        Instance = null;
    }
}
