using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text display;
    void Awake()
    {
        display = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float framerate = (int)(1.0f / Time.deltaTime);
        display.text = framerate.ToString();
    }
}