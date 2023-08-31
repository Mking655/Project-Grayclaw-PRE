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
        public severity GetSeverity() { return severity; }
        public string GetSeverityName() 
        {
            string output = "";
            output += severity.ToString(); 
            switch (severity)//add info explaining CAT rating system
            { 
                case severity.CATI:
                    output += " (Critical)";
                    break;
                case severity.CATII:
                    output += " (Serious)";
                    break;
                case severity.CATIII:
                    output += " (Minor)";
                    break;
            }
            return output;
        }
        [SerializeField]
        private string name;
        public string getName() { return name; }
        [SerializeField]
        private string description;
        public string getDescription() { return description; }
        public STIGerror(severity severity, string name, string description)
        {
            this.severity = severity;
            this.name = name;
            this.description = description;
        }
    }
    [SerializeField]
    private List<STIGerror> errorList;
    public List<STIGerror> getErrorList()
    {
        return errorList;
    }
}
