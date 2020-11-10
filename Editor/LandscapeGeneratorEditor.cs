using UnityEditor;

namespace ProceduralToolkit
{
    [CustomEditor(typeof(LandscapeGenerator))]
    [LayoutPath("Layouts/LandscapeGeneratorEditor")]
    public class LandscapeGeneratorEditor : EditorBase<LandscapeGeneratorEditor>
    {
        private void Awake()
        {
            
        }
    }
}
