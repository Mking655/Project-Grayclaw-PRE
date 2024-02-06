using UnityEngine;

public enum EndpointState { Fixed, Vulnerable, Broken }

public class Endpoint : MonoBehaviour
{
    public EndpointState state = EndpointState.Vulnerable;
    public string criticalFunction; // Description of the endpoint's function
    public GameObject visualIndicator; // Optional: Visual feedback for the state
    public vulnerability vulnerability;

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
        // Update visualIndicator based on the current state
    }
}
[System.Serializable]
public class vulnerability
{
    public GameObject correspondingMinigamePrefab;//must have Minigame component
    public string name;
}