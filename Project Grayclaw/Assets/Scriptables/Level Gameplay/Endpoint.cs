using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum EndpointState { Fixed, Vulnerable, Broken }

public class Endpoint : MonoBehaviour
{
    public EndpointState state = EndpointState.Vulnerable;
    
    public string endpointName;
    public string criticalFunction; // Description of the endpoint's function
    [Tooltip("Endpoint's UI representation")]
    public Animator visualIndicator; // Optional: Visual feedback for the state
    [HideInInspector]
    public vulnerability vulnerability;
    private void Awake()
    {
        if (visualIndicator == null)
        {
            Debug.LogError("Endpoint " + name + " has no ui representation");
        }
        ChangeState(state);
    }
    // Method to change the endpoint state
    public void ChangeState(EndpointState newState)
    {
        if (newState == EndpointState.Fixed)
        {
            vulnerability = null;
        }
        state = newState;
        UpdateVisualIndicator();
        // Additional logic for state change
    }

    void UpdateVisualIndicator()
    {
        if(visualIndicator == null)
        {
            Debug.LogError("endpoint representation does not have a visual indicator");
            return;
        }
        // Update visualIndicator based on the current state
        switch (state) 
        {
            case EndpointState.Fixed:
                visualIndicator.SetInteger("State", 2);
                break;
            case EndpointState.Vulnerable:
                visualIndicator.SetInteger("State", 1);
                break;
            case EndpointState.Broken:
                visualIndicator.SetInteger("State", 0);
                break;
        }
    }
}
[System.Serializable]
public class vulnerability
{
    public GameObject correspondingMinigamePrefab;//must have Minigame component
    public string name;
}