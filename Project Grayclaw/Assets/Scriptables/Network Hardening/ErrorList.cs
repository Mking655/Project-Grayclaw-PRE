using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "My Assets/Error List")]
public class ErrorList : ScriptableObject
{
    [Header("Define Error prefabs here. No two errors can correspond to the same error type")]
    [SerializeField]
    private List<Error> errorsList;


    [HideInInspector]
    public List<ERROR> possibleTypes;

    private Dictionary<ERROR, Error> errors = new Dictionary<ERROR, Error>();

    public Dictionary<ERROR, Error> getErrors()
    {
        updateDictionary();
        return errors;
    }

    public void updateDictionary()
    {
        if (errors == null)
        {
            errors = new Dictionary<ERROR, Error>();
        }

        errors.Clear();
        possibleTypes.Clear();
        HashSet<ERROR> seenErrorKeys = new HashSet<ERROR>();

        foreach (Error error in errorsList)
        {
            ERROR errorKey = error.getErrorKey();
            if (error != null && !seenErrorKeys.Contains(errorKey))
            {
                seenErrorKeys.Add(errorKey);
                possibleTypes.Add(errorKey);
                errors[errorKey] = error;
            }
            else
            {
                Debug.LogWarning("Duplicate or null error for type: " + errorKey + ". Update aborted.");
                errors.Clear();
                return;
            }
        }
    }
}

