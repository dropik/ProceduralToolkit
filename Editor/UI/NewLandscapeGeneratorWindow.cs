using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public class NewLandscapeGeneratorWindow : EditorWindow
    {
        [MenuItem("Window/Procedural Toolkit/New Landscape Generator")]
        public static void ShowNewLandscapeGeneratorWindow()
        {
            GetWindow<NewLandscapeGeneratorWindow>();
        }

        public InspectorElement ParametersElement
        {
            set
            {
                var inspectorRoot = rootVisualElement.Query<VisualElement>("inspectorRoot").First();
                inspectorRoot.Add(value);
            }
        }

        public void CreateGUI()
        {
            var uxml = Resources.Load<VisualTreeAsset>("Layouts/new-landscape-generator-window");
            uxml.CloneTree(rootVisualElement);
        }
    }
}