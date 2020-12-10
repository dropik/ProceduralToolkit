using UnityEditor;
using UnityEngine.UIElements;

namespace ProceduralToolkit
{
    [CustomEditor(typeof(LandscapeGenerator))]
    [LayoutPath("Layouts/landscape-generator-editor")]
    public class LandscapeGeneratorEditor : EditorBase<LandscapeGeneratorEditor>
    {
    }
}
