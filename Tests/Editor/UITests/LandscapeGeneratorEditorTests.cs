using System.Collections;
using NUnit.Framework;
using UnityEngine.UIElements;
using UnityEngine.TestTools;
using ProceduralToolkit.EditorTests.Utils;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class LandscapeGeneratorEditorTests : UITestsBase
    {
        private LandscapeGeneratorEditor editor;
        private LandscapeGenerator target;
        private const string GENERATE_BUTTON_NAME = "generate";
        private const string LENGTH_SLIDER_NAME = "plane-length";
        private const string WIDTH_SLIDER_NAME = "plane-width";

        protected override string RootElementName => "landscape-generator-editor";

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

        [UnityTest]
        public IEnumerator TestHasPlaneLengthSlider()
        {
            AssertThatHasOnlyOneElementWithName<Slider>(LENGTH_SLIDER_NAME);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasPlaneWidthSlider()
        {
            AssertThatHasOnlyOneElementWithName<Slider>(WIDTH_SLIDER_NAME);
            yield return null;
        }
    }
}
