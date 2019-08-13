using System;
using UnityEditor;
using UnityEngine;

namespace Noted.Editor
{
    public class PatchNotesWindow : EditorWindow
    {
        private const string EditorKeyNotesPath = "Noted_NotesPath";
        private const string DefaultNotesPath = "PatchNotes";
        private static string _notesPath;

        [MenuItem("Window/Noted/Noted Editor")]
        public static void Initialize()
        {
            var notesWindow = GetWindow<PatchNotesWindow>();
            notesWindow.titleContent = new GUIContent("Patch Notes");
            if (string.IsNullOrWhiteSpace(_notesPath))
            {
                _notesPath = EditorPrefs.GetString(EditorKeyNotesPath, DefaultNotesPath);
            }
            notesWindow.Show();
        }

        private void OnGUI()
        {

        }
    }
}
