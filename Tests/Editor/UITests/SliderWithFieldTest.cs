using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class SliderWithFieldTest : BaseCustomVETest
    {
        private SliderWithField sliderWithField;

        private const string FIELD_WIDTH = "50px";
        private const float TEST_VALUE = 5f;

        protected override VisualElement CreateTestTarget()
        {
            sliderWithField = new SliderWithField();
            return sliderWithField;
        }

        [Test]
        public void TestHasField()
        {
            var fieldsFound = sliderWithField.Query<FloatField>().ToList();
            Assert.That(fieldsFound.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestFieldHasFixedWidth()
        {
            var field = sliderWithField.Query<FloatField>().First();
            Assert.That(field.style.width.ToString(), Is.EqualTo(FIELD_WIDTH));
        }

        [Test]
        public void TestChangingGlobalValueChangesFieldValue()
        {
            sliderWithField.value = TEST_VALUE;
            var field = sliderWithField.Query<FloatField>().First();
            Assert.That(field.value, Is.EqualTo(TEST_VALUE));
        }

        [Test]
        public void TestChangingFieldValueChangesGlobalValue()
        {
            var field = sliderWithField.Query<FloatField>().First();
            field.value = TEST_VALUE;
            Assert.That(sliderWithField.value, Is.EqualTo(TEST_VALUE));
        }
    }
}
