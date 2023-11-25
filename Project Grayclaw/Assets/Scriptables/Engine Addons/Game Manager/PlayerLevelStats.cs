using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Level Stats")]
public class PlayerLevelStats : ScriptableObject
{
    [Header("Persistent Stats")]
    [SerializeField]
    private float bestTime;

    public float BestTime
    {
        get { return bestTime; }
        set { bestTime = value; }
    }

    [SerializeField]
    private char bestRank;

    public char BestRank
    {
        get { return bestRank; }
        set { bestRank = value; }
    }

    [Header("Session Stats")]
    [SerializeField]
    private float currentTime;

    public float CurrentTime
    {
        get { return currentTime; }
        set { currentTime = value; }
    }

    [Header("Difficulty")]
    [SerializeField]
    private int brokenEndpointNumber;

    public int BrokenEndpointNumber
    {
        get { return brokenEndpointNumber; }
        set { brokenEndpointNumber = value; }
    }
}
