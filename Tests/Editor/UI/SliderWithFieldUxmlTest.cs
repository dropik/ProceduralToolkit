using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UI
{
    public class SliderWithFieldUxmlTest : BaseUxmlCustomVETest
    {
        protected override string TestLayout => "slider-with-field-test";

        private const float TEST_VALUE = 5f;

        [Test]
        public void TestFactorySucceeded()
        {
            var sliderWithFieldFound = RootVisualElement.Query<SliderWithField>().ToList();
            Assert.That(sliderWithFieldFound.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestHasField()
        {
            var fieldFound = RootVisualElement.Query<FloatField>().ToList();
            Assert.That(fieldFound.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestValueParsed()
        {
            var slider = RootVisualElement.Query<SliderWithField>().First();
            var field = RootVisualElement.Query<FloatField>().First();
            Assert.That(slider.value, Is.EqualTo(TEST_VALUE));
            Assert.That(field.value, Is.EqualTo(TEST_VALUE));
        }
    }
}
