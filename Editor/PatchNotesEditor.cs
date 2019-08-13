using System;
using EditorGUIExtensions.Editor;
using Noted.Runtime;
using UnityEditor;
using UnityEngine;

namespace Noted.Editor
{
    [CustomEditor(typeof(PatchNotes))]
    public class PatchNotesEditor : UnityEditor.Editor
    {
        private PatchNotes _patchNotes;
        private float _windowWidth;

        //Icons
        private GUIContent _iconCog;

        //Scroll Positions
        private Vector2 _scrollPosMain;

        private void OnEnable()
        {
            _patchNotes = (PatchNotes)target;

            _iconCog = EditorGUIUtility.IconContent("_Popup");
        }

        public override void OnInspectorGUI()
        {
            PatchNoteSection();
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            KnownIssuesSection();
        }

        #region PATCH NOTES
        private void PatchNoteSection()
        {
            GUILayout.Label("Patch Notes", EditorStyles.boldLabel);
            foreach (var patchNote in _patchNotes.patchNotes)
            {
                PatchNoteEntry(patchNote);
            }

            using (new HorizontalBlock())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Add"))
                {
                    _patchNotes.patchNotes.Add(new PatchNote()
                    {
                        dateLogged = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}"
                    });
                }
            }
        }

        private void PatchNoteEntry(PatchNote patchNote)
        {
            var totalWidth = GUILayoutUtility.GetLastRect().width;
            using (new HorizontalBlock(EditorStyles.helpBox))
            {
                GUILayout.Label(patchNote.dateLogged.ToString(), GUILayout.Width(120));
                //TODO Find a better way that takes into account the scroll bar if visible
                patchNote.message = EditorGUILayout.TextArea(patchNote.message, GUILayout.Width(Screen.width - 182));
                if (GUILayout.Button(_iconCog, GUILayout.Width(24)))
                {
                    var newMenu = new GenericMenu();

                    newMenu.AddItem(new GUIContent("Reset Time"), false, OnResetTimePatchNote, patchNote);

                    newMenu.AddSeparator("");

                    if(_patchNotes.patchNotes.IndexOf(patchNote) != 0)
                    {
                        newMenu.AddItem(new GUIContent("Move Up"), false, OnMoveUpPatchNote, patchNote);
                    }
                    if(_patchNotes.patchNotes.IndexOf(patchNote) != _patchNotes.patchNotes.Count - 1)
                    {
                        newMenu.AddItem(new GUIContent("Move Down"), false, OnMoveDownPatchNote, patchNote);
                    }

                    newMenu.AddSeparator("");

                    newMenu.AddItem(new GUIContent("Delete"), false, OnDeletePatchNote, patchNote);

                    newMenu.ShowAsContext();
                }
            }
        }

        private void OnResetTimePatchNote(object patchNoteObject)
        {
            var patchNote = (PatchNote)patchNoteObject;
            var index = _patchNotes.patchNotes.IndexOf(patchNote);
            patchNote.ResetTime();
            _patchNotes.patchNotes[index] = patchNote;
            Repaint();
        }

        private void OnMoveUpPatchNote(object patchNoteObject)
        {
            var patchNote = (PatchNote)patchNoteObject;
            var index = _patchNotes.patchNotes.IndexOf(patchNote);
            _patchNotes.patchNotes.RemoveAt(index);
            _patchNotes.patchNotes.Insert(index - 1, patchNote);
            Repaint();
        }

        private void OnMoveDownPatchNote(object patchNoteObject)
        {
            var patchNote = (PatchNote)patchNoteObject;
            var index = _patchNotes.patchNotes.IndexOf(patchNote);
            _patchNotes.patchNotes.RemoveAt(index);
            _patchNotes.patchNotes.Insert(index + 1, patchNote);
            Repaint();
        }

        private void OnDeletePatchNote(object patchNoteObject)
        {
            var patchNote = (PatchNote)patchNoteObject;
            _patchNotes.patchNotes.Remove(patchNote);
            Repaint();
        }
        #endregion

        #region KNOWN ISSUES
        private void KnownIssuesSection()
        {
            GUILayout.Label("Known Issues", EditorStyles.boldLabel);
            foreach (var knownIssue in _patchNotes.knownIssues)
            {
                KnownIssueEntry(knownIssue);
            }

            using (new HorizontalBlock())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Add"))
                {
                    _patchNotes.knownIssues.Add(new KnownIssue()
                    {
                        dateLogged = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}"
                    });
                }
            }
        }

        private void KnownIssueEntry(KnownIssue knownIssue)
        {
            var totalWidth = GUILayoutUtility.GetLastRect().width;
            using (new HorizontalBlock(EditorStyles.helpBox))
            {
                GUILayout.Label(knownIssue.dateLogged.ToString(), GUILayout.Width(120));
                knownIssue.message = EditorGUILayout.TextArea(knownIssue.message, GUILayout.Width(Screen.width - 182));
                if (GUILayout.Button(_iconCog, GUILayout.Width(24)))
                {
                    var newMenu = new GenericMenu();

                    newMenu.AddItem(new GUIContent("Reset Time"), false, OnResetTimeKnownIssue, knownIssue);

                    newMenu.AddSeparator("");

                    if(_patchNotes.knownIssues.IndexOf(knownIssue) != 0)
                    {
                        newMenu.AddItem(new GUIContent("Move Up"), false, OnMoveUpKnownIssue, knownIssue);
                    }
                    if(_patchNotes.knownIssues.IndexOf(knownIssue) != _patchNotes.knownIssues.Count - 1)
                    {
                        newMenu.AddItem(new GUIContent("Move Down"), false, OnMoveDownKnownIssue, knownIssue);
                    }

                    newMenu.AddSeparator("");

                    newMenu.AddItem(new GUIContent("Delete"), false, OnDeleteKnownIssue, knownIssue);

                    newMenu.ShowAsContext();
                }
            }
        }

        private void OnResetTimeKnownIssue(object knownIssueObject)
        {
            var knownIssue = (KnownIssue)knownIssueObject;
            var index = _patchNotes.knownIssues.IndexOf(knownIssue);
            knownIssue.ResetTime();
            _patchNotes.knownIssues[index] = knownIssue;
            Repaint();
        }

        private void OnMoveUpKnownIssue(object knownIssueObject)
        {
            var knownIssue = (KnownIssue)knownIssueObject;
            var index = _patchNotes.knownIssues.IndexOf(knownIssue);
            _patchNotes.knownIssues.RemoveAt(index);
            _patchNotes.knownIssues.Insert(index - 1, knownIssue);
            Repaint();
        }

        private void OnMoveDownKnownIssue(object knownIssueObject)
        {
            var knownIssue = (KnownIssue)knownIssueObject;
            var index = _patchNotes.knownIssues.IndexOf(knownIssue);
            _patchNotes.knownIssues.RemoveAt(index);
            _patchNotes.knownIssues.Insert(index + 1, knownIssue);
            Repaint();
        }

        private void OnDeleteKnownIssue(object knownIssueObject)
        {
            var knownIssue = (KnownIssue)knownIssueObject;
            _patchNotes.knownIssues.Remove(knownIssue);
            Repaint();
        }
        #endregion
    }
}
