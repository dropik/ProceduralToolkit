using ProceduralToolkit.Components.Startups;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class MenuEntries
    {
        [MenuItem("GameObject/ProceduralToolkit/Landscape Generator")]
        public static void NewLandscapeGenerator()
        {
            var obj = new GameObject
            {
                name = "Landscape Generator"
            };
            var generator = obj.AddComponent<LandscapeGenerator>();
            generator.RegisterUndo();
        }
    }
}
