using ProceduralToolkit.Landscape;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class MenuEntries
    {
        private static void RegisterUndo(MonoBehaviour obj, string name)
        {
            Undo.RegisterCreatedObjectUndo(obj.gameObject, name);
        }

        [MenuItem("GameObject/Procedural Toolkit/New Landscape Generator")]
        public static void NewLandscapeGenerator()
        {
            var root = new GameObject().AddComponent<LandscapeGenerator>();
            RegisterUndo(root, "New Landscape Generator");
        }
    }
}
