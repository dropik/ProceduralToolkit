using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class SliderWithFieldUxmlTest
    {
        private EditorWindow window;
        private VisualElement RootVisualElement => window.rootVisualElement;

        private const string TEST_LAYOUT = "slider-with-field-test";
        private const float TEST_VALUE = 5f;

        [SetUp]
        public void SetUp()
        {
            window = EditorWindow.CreateWindow<EditorWindow>();
            var uxml = Resources.Load<VisualTreeAsset>(TEST_LAYOUT);
            uxml.CloneTree(RootVisualElement);
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
        }

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
