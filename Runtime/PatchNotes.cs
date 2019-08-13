using System;
using System.Collections.Generic;
using UnityEngine;

namespace Noted.Runtime
{
    [CreateAssetMenu]
    public class PatchNotes : ScriptableObject
    {
        [SerializeField] private List<PatchNote> _patchNotes = new List<PatchNote>();
        public List<PatchNote> patchNotes => _patchNotes;

        [SerializeField] private List<KnownIssue> _knownIssues = new List<KnownIssue>();
        public List<KnownIssue> knownIssues => _knownIssues;
    }

    [Serializable]
    public partial struct PatchNote
    {
        public string dateLogged;
        public string message;

        public void ResetTime()
        {
            dateLogged = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}";
        }
    }

    [Serializable]
    public partial struct KnownIssue
    {
        public string dateLogged;
        public string message;

        public void ResetTime()
        {
            dateLogged = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}";
        }
    }
}
