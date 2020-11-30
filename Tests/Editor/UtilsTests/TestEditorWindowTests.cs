using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using ProceduralToolkit.EditorTests.Utils;

namespace ProceduralToolkit.EditorTests.UtilsTests
{
    public class TestEditorWindowTests
    {
        private TestEditorWindow window;
        private GameObject target;
        private SomeEditor editor;
        private const string ELEMENT_NAME = "test-element";
        private const string STYLESHEET_NAME = "test-stylesheet";

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            target = new GameObject();
            editor = Editor.CreateEditor(target, typeof(SomeEditor)) as SomeEditor;
            window = TestEditorWindow.CreateWindow(editor);
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(target);
            window.Close();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestRootElementIsNotNull()
        {
            Assert.That(window.rootVisualElement != null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestEditorIsAssigned()
        {
            Assert.That(window.EditorTarget.Equals(editor));
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestWindowOnNullEditor()
        {
            window.EditorTarget = null;
            Assert.That(window.rootVisualElement != null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestReassigningEditorClearsPreviousVisualTree()
        {
            window.EditorTarget = editor;
            var elementQuery = window.rootVisualElement.Query<VisualElement>(name: ELEMENT_NAME).ToList();
            Assert.That(elementQuery.Count == 1);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestStyleSheetIsAdded()
        {
            AssertThatHasOnlyOnceTestStyleSheet();
            yield return null;
        }

        private void AssertThatHasOnlyOnceTestStyleSheet()
        {
            var styleSheets = window.rootVisualElement.styleSheets;
            var styleSheetsFound = 0;
            for (int i = 0; i < styleSheets.count; i++)
            {
                if (styleSheets[i].name == STYLESHEET_NAME)
                {
                    styleSheetsFound++;
                }
            }
            Assert.That(styleSheetsFound == 1);
        }

        [UnityTest]
        public IEnumerator TestStyleSheetIsNotDuplicated()
        {
            window.EditorTarget = editor;
            AssertThatHasOnlyOnceTestStyleSheet();
            yield return null;
        }

        internal class SomeEditor : Editor
        {
            public override VisualElement CreateInspectorGUI()
            {
                var root = new VisualElement() { name = ELEMENT_NAME };
                root.styleSheets.Add(Resources.Load<StyleSheet>(STYLESHEET_NAME));
                return root;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object other)
            {
                return other.GetType() == typeof(SomeEditor);
            }
        }
    }
}