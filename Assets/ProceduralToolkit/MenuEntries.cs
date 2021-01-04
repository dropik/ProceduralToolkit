using ProceduralToolkit.Components;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class MenuEntries
    {
        [MenuItem("GameObject/Procedural Toolkit/New Landscape Generator")]
        public static void NewLandscapeGenerator()
        {
            var root = new GameObject().AddComponent<LandscapeGenerator>();
            root.RegisterUndo();
        }
    }
}
