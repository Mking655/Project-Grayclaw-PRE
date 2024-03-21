using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPrompt : MonoBehaviour
{
    public GameObject prompt;
    CursorLockMode previousLockMode;
    void Awake()
    {
        if(prompt == null)
        {
            Debug.LogError("did not define prompt for QuitPrompt");
        }
        prompt.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            prompt.SetActive(true);
            previousLockMode = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Deny()
    {
        prompt.SetActive(false);
        Cursor.lockState = previousLockMode;
    }
}
