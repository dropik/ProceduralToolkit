using NUnit.Framework;
using ProceduralToolkit.EditorTests.Utils;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public abstract class UITestsBase
    {
        private TestEditorWindow testingWindow;

        protected VisualElement RootVisualElement => testingWindow.rootVisualElement;

        [UnitySetUp]
        public virtual IEnumerator SetUp()
        {
            var editor = CreateEditor();
            testingWindow = TestEditorWindow.CreateWindow(editor);
            yield return null;
        }

        protected abstract Editor CreateEditor();

        [UnityTearDown]
        public virtual IEnumerator TearDown()
        {
            testingWindow.Close();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestRootElementIsNotNull()
        {
            Assert.That(RootVisualElement != null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestCorrectLayoutIsLoaded()
        {
            AssertThatHasOnlyOneElementWithName<VisualElement>(RootElementName);
            yield return null;
        }

        protected void AssertThatHasOnlyOneElementWithName<TElement>(string name)
            where TElement : VisualElement
        {
            var root = RootVisualElement;
            var rootElementQuery = root.Query<TElement>(name).ToList();
            Assert.That(rootElementQuery.Count == 1);
        }

        protected abstract string RootElementName { get; }
    }
}
