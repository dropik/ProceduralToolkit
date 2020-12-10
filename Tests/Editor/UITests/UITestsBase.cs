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
        private const string COMMON_EDITOR_STYLESHEET = "common-editor";

        protected VisualElement RootVisualElement => testingWindow.rootVisualElement;

        [UnitySetUp]
        public virtual IEnumerator SetUp()
        {
            var editor = CreateEditor();
            testingWindow = TestEditorWindow.CreateWindow(editor);
            yield return null;
        }

        protected abstract Editor CreateEditor();

        protected TElement AssertThatHasOnlyOneElementWithName<TElement>(string name)
            where TElement : VisualElement
        {
            var root = RootVisualElement;
            var rootElementQuery = root.Query<TElement>(name).ToList();
            Assert.That(rootElementQuery.Count == 1, "Was not able to find exaclty 1 element.");
            return rootElementQuery[0];
        }

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
        public IEnumerator TestCommonEditorStyleSheetIsLoaded()
        {
            AssertThatHasStyleSheetWithName(COMMON_EDITOR_STYLESHEET);
            yield return null;
        }

        private void AssertThatHasStyleSheetWithName(string name)
        {
            var styleSheets = RootVisualElement.styleSheets;
            var styleSheetIsLoaded = false;
            for (int i = 0; i < styleSheets.count; i++)
            {
                var curStyleSheet = styleSheets[i];
                if (curStyleSheet.name == name)
                {
                    styleSheetIsLoaded = true;
                    break;
                }
            }
            Assert.That(styleSheetIsLoaded);
        }
    }
}
