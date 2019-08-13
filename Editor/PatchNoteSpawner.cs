using Noted.Runtime;
using UnityEditor;
using UnityEngine;

namespace Noted.Editor
{
    public static class PatchNoteSpawner
    {
        private const string DefaultPatchNotesPath = "PatchNotes";

        private static PatchNotes _patchNotes;
        public static PatchNotes PatchNotes => _patchNotes;

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            _patchNotes = Resources.Load<PatchNotes>(DefaultPatchNotesPath);
            if (_patchNotes == null)
            {
                Debug.Log("<b>PatchNotes: </b>Default asset not found. Creating on automatically");
                var newPatchNotes = new PatchNotes();
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }
                AssetDatabase.Refresh();
                AssetDatabase.CreateAsset(newPatchNotes, "Assets/Resources/PatchNotes.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }
}
