using UnityEditor;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.Utils
{
    public class TestEditorWindow : EditorWindow
    {
        private Editor editorTarget;
        private VisualElement editorRoot;

        public Editor EditorTarget
        {
            get => editorTarget;
            set
            {
                if (editorTarget != null)
                {
                    RemovePreviousStyleSheets();
                }
                editorTarget = value;
                UpdateRootVisualElement();
            }
        }

        private void RemovePreviousStyleSheets()
        {
            var styleSheets = editorRoot.styleSheets;
            for (int i = 0; i < styleSheets.count; i++)
            {
                rootVisualElement.styleSheets.Remove(styleSheets[i]);
            }
        }

        private void UpdateRootVisualElement()
        {
            rootVisualElement.Clear();
            if (EditorTarget != null)
            {
                IntegrateEditorToWindowRoot();
            }
        }

        private void IntegrateEditorToWindowRoot()
        {
            editorRoot = EditorTarget.CreateInspectorGUI();
            rootVisualElement.Add(editorRoot);
            UpdateStyleSheets();
        }

        private void UpdateStyleSheets()
        {
            var styleSheets = editorRoot.styleSheets;
            for (int i = 0; i < styleSheets.count; i++)
            {
                rootVisualElement.styleSheets.Add(styleSheets[i]);
            }
        }

        public static TestEditorWindow CreateWindow(Editor editor)
        {
            var newWindow = GetWindow<TestEditorWindow>();
            newWindow.EditorTarget = editor;
            return newWindow;
        }
    }
}
