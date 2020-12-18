using NUnit.Framework;
using UnityEditor;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public abstract class BaseCustomVETest
    {
        private EditorWindow window;

        protected VisualElement RootVisualElement => window.rootVisualElement;

        [SetUp]
        public void SetUp()
        {
            PreWindowCreation();
            var testTarget = CreateTestTarget();
            window = EditorWindow.CreateWindow<EditorWindow>();
            window.rootVisualElement.Add(testTarget);
            PostWindowCreation();
        }

        protected virtual void PreWindowCreation() { }
        protected abstract VisualElement CreateTestTarget();

        protected virtual void PostWindowCreation() { }

        [TearDown]
        public void TearDown()
        {
            PreWindowClose();
            window.Close();
        }

        protected virtual void PreWindowClose() { }
    }
}