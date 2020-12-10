using System.Collections;
using NUnit.Framework;
using UnityEngine.UIElements;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class LandscapeGeneratorEditorTests : UITestsBase
    {
        private LandscapeGeneratorEditor editor;
        private LandscapeGenerator target;
        private const string GENERATE_BUTTON_NAME = "generate";

        protected override Editor CreateEditor()
        {
            target = new GameObject().AddComponent<LandscapeGenerator>();
            editor = Editor.CreateEditor(target, typeof(LandscapeGeneratorEditor)) as LandscapeGeneratorEditor;
            return editor;
        }

        [UnityTearDown]
        public override IEnumerator TearDown()
        {
            yield return base.TearDown();
            Object.DestroyImmediate(target.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasGenerateButton()
        {
            AssertThatHasOnlyOneElementWithName<Button>(GENERATE_BUTTON_NAME);
            yield return null;
        }
    }
}
