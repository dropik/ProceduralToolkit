using ProceduralToolkit.Components.Startups;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class MenuEntries
    {
        [MenuItem("GameObject/ProceduralToolkit/New LandscapeGenerator")]
        public static void NewLandscapeGenerator()
        {
            Create<LandscapeGenerator>();
        }

        public static void Create<TStartup>() where TStartup : Startup
        {
            var startup = new GameObject().AddComponent<TStartup>();
            startup.RegisterUndo();
        }
    }
}
