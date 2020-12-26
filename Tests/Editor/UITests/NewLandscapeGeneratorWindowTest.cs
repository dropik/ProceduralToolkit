using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;
using ProceduralToolkit.EditorTests.Utils;
using Moq;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

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
        private Mock<IGeneratorBootProvider> mockGeneratorBootProvider;
        private TestBaseShapeGeneratorSettings testObject;

        private const string TEST_OBJECT_NAME = "Test Object";

        private VisualElement Root =>
            window.rootVisualElement;

        private Button CreateButton =>
            Root.Query<Button>("createGenerator").First();

        private InspectorElement ParametersElement =>
            Root.Query<InspectorElement>().First();

        [SetUp]
        public void SetUp()
        {
            mockGeneratorBootProvider = new Mock<IGeneratorBootProvider>();
            testObject = ScriptableObject.CreateInstance<TestBaseShapeGeneratorSettings>();
            testObject.name = TEST_OBJECT_NAME;
            window = EditorWindow.CreateWindow<NewLandscapeGeneratorWindow>();
        }

        private void SetAllFields()
        {
            SetBaseShape();
            SetGeneratorBootProvider();
        }

        private void SetBaseShape()
        {
            window.baseShape = testObject;
            window.OnInspectorUpdate();
        }

        private void SetGeneratorBootProvider()
        {
            window.GeneratorBootProvider = mockGeneratorBootProvider.Object;
            window.OnInspectorUpdate();
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
            Object.DestroyImmediate(testObject);
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
            SetGeneratorBootProvider();
            Assert.That(CreateButton.enabledSelf, Is.False);
        }

        [Test]
        public void TestCreateGeneratorIsNotActiveWhenProviderIsNull()
        {
            SetBaseShape();
            Assert.That(CreateButton.enabledSelf, Is.False);
        }

        [Test]
        public void TestCreateGeneratorIsActiveWhenAllFieldsNotNull()
        {
            SetAllFields();
            Assert.That(CreateButton.enabledSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator TestGeneratorBootProviderUsedToCreateGenerator()
        {
            SetAllFields();
            var buttonClicker = new ButtonClicker();

            buttonClicker.Click(CreateButton);

            yield return SkipFrames();

            mockGeneratorBootProvider.Verify(
                m => m.GetGeneratorBoot(It.Is<BaseShapeGeneratorSettings>(s => s.name == TEST_OBJECT_NAME)),
                Times.Once);
        }
    }
}