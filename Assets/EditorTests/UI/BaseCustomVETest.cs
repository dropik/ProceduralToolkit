using NUnit.Framework;
using UnityEditor;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UI
{
    public abstract class BaseCustomVETest
    {
        private EditorWindow window;

        protected VisualElement RootVisualElement => window.rootVisualElement;

        [SetUp]
        public void SetUp()
        {
            var testTarget = CreateTestTarget();
            window = EditorWindow.CreateWindow<EditorWindow>();
            window.rootVisualElement.Add(testTarget);
        }
        
        protected abstract VisualElement CreateTestTarget();

        [TearDown]
        public void TearDown()
        {
            window.Close();
        }
    }
}