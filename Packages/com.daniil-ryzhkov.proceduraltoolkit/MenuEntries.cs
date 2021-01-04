using ProceduralToolkit.Components;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class MenuEntries
    {
        [MenuItem("GameObject/ProceduralToolkit/New LandscapeGenerator")]
        public static void NewLandscapeGenerator()
        {
            var root = new GameObject().AddComponent<LandscapeGenerator>();
            root.RegisterUndo();
        }
    }
}
