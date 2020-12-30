using ProceduralToolkit.UI;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    [ExecuteInEditMode]
    public class CompositionRoot : ScriptableObject
    {
        public void OnValidate()
        {
        }

        [MenuItem("Window/Procedural Toolkit/New Landscape Generator")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow<NewLandscapeGeneratorWindow>(
                    title: "New Landscape Generator",
                    utility: true
            );
            window.GeneratorBootFactory = new GeneratorBootFactory();
        }
    }
}
