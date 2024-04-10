using UnityEngine;

public static class PlayerSettings
{
    private const string FovKey = "FOV";
    private const string InteractingFovKey = "InteractingFOV";

    public static int FOV
    {
        get => PlayerPrefs.GetInt(FovKey, 80); // Default FOV is 80
        set => PlayerPrefs.SetInt(FovKey, value);
    }

    public static int InteractingFOV
    {
        get => PlayerPrefs.GetInt(InteractingFovKey, 45); // Default Interacting FOV is 45
        set => PlayerPrefs.SetInt(InteractingFovKey, value);
    }

    // Call this when you want to save changes
    public static void Save()
    {
        PlayerPrefs.Save();
    }
}
