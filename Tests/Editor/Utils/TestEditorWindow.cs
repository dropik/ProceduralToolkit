using UnityEditor;

namespace ProceduralToolkit.EditorTests.Utils
{
    public class TestEditorWindow : EditorWindow
    {
        public Editor EditorTarget
        {
            get => editorTarget;
            set
            {
                editorTarget = value;
                UpdateRootVisualElement();
            }
        }
        private Editor editorTarget;

        public static TestEditorWindow CreateWindow(Editor editor)
        {
            var newWindow = GetWindow<TestEditorWindow>();
            newWindow.EditorTarget = editor;
            return newWindow;
        }

        private void UpdateRootVisualElement()
        {
            rootVisualElement.Clear();
            rootVisualElement.Add(EditorTarget.CreateInspectorGUI());
        }
    }
}
