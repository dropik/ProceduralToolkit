using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public class NewLandscapeGeneratorWindow : EditorWindow
    {
        [MenuItem("Window/Procedural Toolkit/New Landscape Generator")]
        public static void ShowExample()
        {
            var wnd = GetWindow<NewLandscapeGeneratorWindow>();
            wnd.titleContent = new GUIContent("New Landscape Generator");
        }

        private GeneratorBootDescriptor boot;

        private void Awake()
        {
            boot = CreateInstance<GeneratorBootDescriptor>();
        }

        private void OnDestroy()
        {
            if (boot != null)
            {
                DestroyImmediate(boot);
            }
        }

        public void CreateGUI()
        {
            LoadLayout();
            AddInspectorElement();
        }

        private void LoadLayout()
        {
            var visualTree = Resources.Load<VisualTreeAsset>("Layouts/new-landscape-generator-window");
            visualTree.CloneTree(rootVisualElement);
        }

        private void AddInspectorElement()
        {
            var inspectorElement = new InspectorElement(boot);
            var inspectorRoot = rootVisualElement.Query("inspectorRoot").First();
            inspectorRoot.Add(inspectorElement);
        }
    }
}
