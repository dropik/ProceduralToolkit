using System.Collections;
using NUnit.Framework;
using ProceduralToolkit.Landscape;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.E2E
{
    public class LandscapeGeneratorE2E
    {
        private Scene testScene;

        private const string TEST_SCENE = "Assets/ProceduralToolkit/Tests/Editor/E2E/Scenes/LandscapeGeneratorE2E.unity";
        private const float TEST_LENGTH = 2f;

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
            yield return ExecuteUndo();
            AssertGeneratorBootEliminated();
        }

        private IEnumerator ExecuteUndo()
        {
            Undo.PerformUndo();
            yield return SkipFrames();
        }

        private void AssertGeneratorBootEliminated()
        {
            Assert.That(Boot == null);
        }

        [UnityTest]
        public IEnumerator TestOnPlaneSettingsUpdate()
        {
            yield return ChangeLength();
            AssertNewMeshLength();
        }

        private IEnumerator ChangeLength()
        {
            var plane = Boot.GetComponent<PlaneSettings>();
            plane.length = TEST_LENGTH;
            plane.OnValidate();

            yield return SkipFrames();
        }

        private void AssertNewMeshLength()
        {
            var resultingMesh = Boot.GetComponent<MeshFilter>().sharedMesh;
            var vertices = resultingMesh.vertices;
            var resultingLength = Vector3.Distance(vertices[0], vertices[1]);
            Assert.That(resultingLength, Is.EqualTo(TEST_LENGTH));
        }
    }
}
