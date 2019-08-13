using System;
using Noted.Runtime;
using UnityEditor;
using UnityEngine;

namespace Noted.Editor
{
    public class PatchNotesWindow : EditorWindow
    {
        private PatchNotes _patchNotes;
        private UnityEditor.Editor _patchNoteEditor;

        [MenuItem("Window/Noted/Noted Editor")]
        public static void Initialize()
        {
            var notesWindow = GetWindow<PatchNotesWindow>();
            notesWindow.titleContent = new GUIContent("Patch Notes");
            notesWindow.Show();
        }

        private void OnEnable()
        {
            _patchNotes = PatchNoteSettings.PatchNotes;
            _patchNoteEditor = UnityEditor.Editor.CreateEditor(_patchNotes);
        }

        private void OnGUI()
        {
            _patchNoteEditor.OnInspectorGUI();
        }
    }
}
