using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum EndpointState { Fixed, Vulnerable, Broken }

public class Endpoint : MonoBehaviour
{
    public EndpointState state = EndpointState.Vulnerable;
    public string criticalFunction; // Description of the endpoint's function
    public GameObject visualIndicator; // Optional: Visual feedback for the state
    [HideInInspector]
    public vulnerability vulnerability;
    private void Start()
    {
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
        if(gameObject.GetComponent<Image>() == null)
        {
            Debug.Log("temporary endpoint representation doesnt have an image");
            return;
        }
        Image image = gameObject.GetComponent<Image>();
        // Update visualIndicator based on the current state
        switch (state) 
        {
            case EndpointState.Fixed:
                image.color = Color.green;
                break;
            case EndpointState.Vulnerable:
                image.color = Color.yellow;
                break;
            case EndpointState.Broken:
                image.color = Color.red;
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