using NUnit.Framework;
using ProceduralToolkit.UI;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine.TestTools;
using UnityEditor;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class SliderWithFieldTest
    {
        private SliderWithField sliderWithField;
        private EditorWindow window;

        private const string FIELD_WIDTH = "50px";
        private const float TEST_VALUE = 5f;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            sliderWithField = new SliderWithField();
            window = EditorWindow.CreateWindow<EditorWindow>();
            window.rootVisualElement.Add(sliderWithField);
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            window.Close();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestHasField()
        {
            var fieldsFound = sliderWithField.Query<FloatField>().ToList();
            Assert.AreEqual(1, fieldsFound.Count);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestFieldHasFixedWidth()
        {
            var field = sliderWithField.Query<FloatField>().First();
            Assert.AreEqual(FIELD_WIDTH, field.style.width.ToString());
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestChangingGlobalValueChangesFieldValue()
        {
            sliderWithField.value = TEST_VALUE;

            yield return SkipFrames();

            var field = sliderWithField.Query<FloatField>().First();
            Assert.AreEqual(TEST_VALUE, field.value);
        }

        [UnityTest]
        public IEnumerator TestChangingFieldValueChangesGlobalValue()
        {
            var field = sliderWithField.Query<FloatField>().First();
            field.value = TEST_VALUE;

            yield return SkipFrames();

            Assert.AreEqual(TEST_VALUE, sliderWithField.value);
        }
    }
}
