using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simmaler to PlayerPrefs, but contains all the game-specific settings the player has defined instead
/// </summary>
public static class PlayerSettings
{
    /// <summary>
    /// FOV when player is in first person mode
    /// </summary>
    public static int FOV = 80;
    /// <summary>
    /// FOV when player is in interacting mode
    /// </summary>
    public static int InteractingFOV = 45;
}
