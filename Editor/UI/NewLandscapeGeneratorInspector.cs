using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    [CustomEditor(typeof(NewLandscapeGeneratorWindow))]
    public class NewLandscapeGeneratorInspector : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var baseShapeField = new PropertyField()
            {
                name = "baseShape",
                bindingPath = "baseShape"
            };
            root.Add(baseShapeField);
            return root;
        }
    }
}
