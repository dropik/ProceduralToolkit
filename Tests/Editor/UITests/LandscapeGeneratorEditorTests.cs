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
        private const string LENGTH_SLIDER_NAME = "plane-length-slider";
        private const string WIDTH_SLIDER_NAME = "plane-width-slider";
        private const string LENGTH_FIELD_NAME = "plane-length-field";
        private const string WIDTH_FIELD_NAME = "plane-width-field";
        private const float TEST_FLOAT_VALUE = 5f;
        private const float FIELD_MAX_VALUE = 100f;
        private const float SLIDER_STEP = 0.01f;
        private const int SLIDER_MAX_VALUE = (int)(FIELD_MAX_VALUE / SLIDER_STEP);
        private const int TEST_SLIDER_VALUE = (int)(TEST_FLOAT_VALUE / SLIDER_STEP);

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
            const float expectedLowValue = 0;
            const float expectedHighValue = SLIDER_MAX_VALUE;
            var slider = AssertThatHasOnlyOneElementWithName<SliderInt>(LENGTH_SLIDER_NAME);
            Assert.That(slider.lowValue == expectedLowValue, "Low value is not correct");
            Assert.That(slider.highValue == expectedHighValue, "High value is not correct");
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasPlaneWidthSlider()
        {
            AssertThatHasOnlyOneElementWithName<Slider>(WIDTH_SLIDER_NAME);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasPlaneLengthField()
        {
            AssertThatHasOnlyOneElementWithName<FloatField>(LENGTH_FIELD_NAME);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasPlaneWidthField()
        {
            AssertThatHasOnlyOneElementWithName<FloatField>(WIDTH_FIELD_NAME);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestSliderPlaneLengthBinding()
        {
            var lengthSlider = RootVisualElement.Query<SliderInt>(name: LENGTH_SLIDER_NAME).First();
            var lengthField = RootVisualElement.Query<FloatField>(name: LENGTH_FIELD_NAME).First();
            lengthSlider.value = TEST_SLIDER_VALUE;

            yield return SkipFrames();

            Assert.That(target.planeLength == TEST_FLOAT_VALUE, "Object value was not set.");
            Assert.That(lengthField.value == TEST_FLOAT_VALUE, "Field value was not set.");
        }
    }
}
