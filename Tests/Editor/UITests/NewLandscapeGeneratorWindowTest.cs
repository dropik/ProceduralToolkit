using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class NewLandscapeGeneratorWindowTest
    {
        internal class TestBaseShapeGeneratorSettings : BaseShapeGeneratorSettings
        {
            protected override IGenerator CreateGenerator()
            {
                return null;
            }
        }

        private NewLandscapeGeneratorWindow window;

        private VisualElement Root =>
            window.rootVisualElement;

        private Button CreateButton =>
            Root.Query<Button>("createGenerator").First();

        private InspectorElement ParametersElement =>
            Root.Query<InspectorElement>().First();

        [SetUp]
        public void SetUp()
        {
            window = EditorWindow.CreateWindow<NewLandscapeGeneratorWindow>();
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
        }

        [Test]
        public void TestHasCreateGeneratorButton()
        {
            Assert.That(CreateButton, Is.Not.Null);
        }

        [Test]
        public void TestInspectorElementAdded()
        {
            Assert.That(ParametersElement, Is.Not.Null);
        }

        [Test]
        public void TestCreateGeneratorIsNotActiveWhenBaseShapeIsNull()
        {
            Assert.That(CreateButton.enabledSelf, Is.False);
        }

        [Test]
        public void TestCreateGeneratorIsActiveWhenBaseShapeIsNotNull()
        {
            var testObject = ScriptableObject.CreateInstance<TestBaseShapeGeneratorSettings>();
            window.baseShape = testObject;

            window.OnValidate();

            Assert.That(CreateButton.enabledSelf, Is.True);

            Object.DestroyImmediate(testObject);
        }
    }
}