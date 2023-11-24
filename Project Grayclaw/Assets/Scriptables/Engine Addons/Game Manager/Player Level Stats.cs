using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Level Stats")]
public class PlayerLevelStats : ScriptableObject
{
    [Header("Persistent Stats")]
    [SerializeField]
    private float bestTime;
    [SerializeField]
    private char bestRank;

    [Header("Session Stats")]
    [SerializeField]
    private float currentTime;
    private int BrokenEndpointNumber;

    public float BestTime { get => bestTime; set => bestTime = value; }
    public char BestRank { get => bestRank; set => bestRank = value; }
    public float CurrentTime { get => currentTime; set => currentTime = value; }
    public int BrokenEndpointNumber1 { get => BrokenEndpointNumber; set => BrokenEndpointNumber = value; }
}
