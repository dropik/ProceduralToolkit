using System.Collections;
using NUnit.Framework;
using ProceduralToolkit.EditorTests.Utils;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.E2E
{
    public class LandscapeGeneratorBuildingSimplePlaneE2E
    {
        private NewLandscapeGeneratorWindow window;
        private GeneratorBoot boot;
        private LandscapeGenerator landscapeGenerator;

        [SetUp]
        public void SetUp()
        {
            CompositionRoot.ShowWindow();
            window = EditorWindow.GetWindow<NewLandscapeGeneratorWindow>();
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
            if (boot != null)
            {
                Object.DestroyImmediate(boot.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator TestLandscapeGeneratorBuildingSimplePlane()
        {
            Arrange();
            yield return SkipFrames();
            Act();
            yield return SkipFrames();
            AssertGeneration();
        }

        private void Arrange()
        {
            var baseShape = Resources.Load<PlaneGeneratorSettings>("TestPlaneSettings");
            window.baseShape = baseShape;
        }

        private void Act()
        {
            var buttonClicker = new ButtonClicker();
            var createButton = window.rootVisualElement.Query<Button>("createGenerator").First();
            buttonClicker.Click(createButton);
        }

        private void AssertGeneration()
        {
            AssertBootCreated();
            AssertLandscapeGeneratorCreated();
            AssertMeshCreatedCorrectly();
            AssertMaterialAssigned();
        }

        private void AssertBootCreated()
        {
            boot = Object.FindObjectOfType<GeneratorBoot>();
            Assert.That(boot, Is.Not.Null);
        }

        private void AssertLandscapeGeneratorCreated()
        {
            landscapeGenerator = boot.GetComponentInChildren<LandscapeGenerator>();
            Assert.That(landscapeGenerator, Is.Not.Null);
        }

        private void AssertMeshCreatedCorrectly()
        {
            var generatedMesh = landscapeGenerator.GetComponent<MeshFilter>().sharedMesh;
            Assert.That(generatedMesh, Is.Not.Null);
            Assert.That(generatedMesh.vertexCount, Is.EqualTo(6));
        }

        private void AssertMaterialAssigned()
        {
            var material = landscapeGenerator.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(material, Is.Not.Null);
        }
    }
}
