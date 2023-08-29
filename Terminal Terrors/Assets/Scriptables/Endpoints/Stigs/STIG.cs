using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum severity
{
    CATI,
    CATII, 
    CATIII
}
[CreateAssetMenu(menuName = "STIG")]
/// <summary>
/// The base class for a Secure Technical Implementation Guide. Contains the list of all possible errors for a component of an endpoint.
/// </summary>
public class STIG : ScriptableObject
{
    [System.Serializable]
    public class STIGerror
    {
        [SerializeField]
        private severity severity;
        [SerializeField]
        private string name;
        [SerializeField]
        private string description;
        public STIGerror(severity severity, string name, string description)
        {
            this.severity = severity;
            this.name = name;
            this.description = description;
        }
    }
    [SerializeField]
    public List<STIGerror> errorList { get; private set; }
}
