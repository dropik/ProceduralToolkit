using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public class NewLandscapeGeneratorWindow : EditorWindow
    {
        [MenuItem("Window/Procedural Toolkit/New Landscape Generator")]
        public static void ShowWindow()
        {
            GetWindow<NewLandscapeGeneratorWindow>(title: "New Landscape Generator",
                                                   utility: true);
        }

        public BaseShapeGeneratorSettings baseShape;

        public void CreateGUI()
        {
            LoadLayout();
            AddInspector();
        }

        private void LoadLayout()
        {
            var uxml = Resources.Load<VisualTreeAsset>("Layouts/new-landscape-generator-window");
            uxml.CloneTree(rootVisualElement);
        }

        private void AddInspector()
        {
            var inspectorRoot = rootVisualElement.Query<VisualElement>("inspectorRoot").First();
            inspectorRoot.Add(new InspectorElement(this));
        }

        public void OnFocus()
        {
            UpdateButtonActiveState();
        }

        public void OnValidate()
        {
            UpdateButtonActiveState();
        }

        private void UpdateButtonActiveState()
        {
            CreateButton.SetEnabled(IsFormValidated);
        }

        private Button CreateButton =>
            rootVisualElement.Query<Button>("createGenerator").First();

        private bool IsFormValidated =>
            baseShape != null;
    }
}