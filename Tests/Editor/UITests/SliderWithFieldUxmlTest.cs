using NUnit.Framework;
using ProceduralToolkit.UI;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class SliderWithFieldUxmlTest
    {
        private EditorWindow window;

        private const string TEST_LAYOUT = "slider-with-field-test";

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            window = EditorWindow.CreateWindow<EditorWindow>();
            var uxml = Resources.Load<VisualTreeAsset>(TEST_LAYOUT);
            uxml.CloneTree(window.rootVisualElement);
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            window.Close();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestFactorySucceeded()
        {
            var sliderWithFieldFound = window.rootVisualElement.Query<SliderWithField>().ToList();
            Assert.AreEqual(1, sliderWithFieldFound.Count);
            yield return null;
        }
    }
}
