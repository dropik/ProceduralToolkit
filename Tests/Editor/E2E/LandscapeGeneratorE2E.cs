using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.E2E
{
    public class LandscapeGeneratorE2E
    {
        private Scene testScene;

        private const string TEST_SCENE = "Assets/ProceduralToolkit/Tests/Editor/E2E/Scenes/LandscapeGeneratorE2E.unity";

        private GameObject Boot =>
            GameObject.Find("LandscapeGenerator");

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            yield return OpenScene();
            yield return ComposeGenerator();
        }

        private IEnumerator OpenScene()
        {
            testScene = EditorSceneManager.OpenScene(TEST_SCENE);
            yield return SkipFrames();
        }

        private IEnumerator ComposeGenerator()
        {
            MenuEntries.NewLandscapeGenerator();
            yield return SkipFrames();
        }

        [TearDown]
        public void TearDown()
        {
            if (Boot != null)
            {
                Object.DestroyImmediate(Boot);
                EditorSceneManager.SaveScene(testScene);
            }
        }

        [Test]
        public void TestNewLandscapeGeneratorBuildingSimplePlane()
        {
            AssertGeneration();
        }

        private void AssertGeneration()
        {
            AssertBootCreated();
            AssertMeshCreatedCorrectly();
            AssertMaterialAssigned();
        }

        private void AssertBootCreated()
        {
            Assert.That(Boot, Is.Not.Null);
        }

        private void AssertMeshCreatedCorrectly()
        {
            var generatedMesh = Boot.GetComponent<MeshFilter>().sharedMesh;
            Assert.That(generatedMesh, Is.Not.Null);
            Assert.That(generatedMesh.vertexCount, Is.EqualTo(6));
        }

        private void AssertMaterialAssigned()
        {
            var material = Boot.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(material, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator TestNewLandscapeGeneratorSavedOnSceneReload()
        {
            yield return ReloadScene();
            AssertGeneration();
        }

        private IEnumerator ReloadScene()
        {
            EditorSceneManager.SaveScene(testScene);
            yield return OpenScene();
        }

        [UnityTest]
        public IEnumerator TestNewLandscapeGeneratorUndo()
        {
            yield return Act();
            AssertGeneratorBootEliminated();
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
