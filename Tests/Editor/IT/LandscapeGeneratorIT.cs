using System.Collections;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Api;
using ProceduralToolkit.EditorTests.Utils;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.IT
{
    public class LandscapeGeneratorIT
    {
        internal class MockBaseShape : BaseShapeGeneratorSettings
        {
            private readonly Mock<IGenerator> mockGenerator = new Mock<IGenerator>();

            protected override IGenerator CreateGenerator()
            {
                return mockGenerator.Object;
            }
        }

        private NewLandscapeGeneratorWindow window;
        private MockBaseShape mockBaseShape;

        private GameObject Boot =>
            GameObject.Find("GeneratorBoot");

        [SetUp]
        public void SetUp()
        {
            NewLandscapeGeneratorWindow.ShowWindow();
            window = EditorWindow.GetWindow<NewLandscapeGeneratorWindow>();

            mockBaseShape = ScriptableObject.CreateInstance<MockBaseShape>();
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
            Object.DestroyImmediate(mockBaseShape);
        }

        [UnityTest]
        public IEnumerator TestNewLandscapeGeneratorUndo()
        {
            yield return Arrange();
            yield return Act();
            AssertGeneratorBootEliminated();
        }

        private IEnumerator Arrange()
        {
            yield return SetupWindow();
            yield return ClickCreateGeneratorButton();
        }

        private IEnumerator SetupWindow()
        {
            window.baseShape = mockBaseShape;
            yield return SkipFrames();
        }

        private IEnumerator ClickCreateGeneratorButton()
        {
            var buttonClicker = new ButtonClicker();
            var createButton = window.rootVisualElement.Query<Button>("createGenerator").First();
            buttonClicker.Click(createButton);
            yield return SkipFrames();
        }

        private IEnumerator Act()
        {
            Undo.PerformUndo();
            yield return SkipFrames();
        }

        private void AssertGeneratorBootEliminated()
        {
            Assert.That(Boot == null);
        }
    }
}
