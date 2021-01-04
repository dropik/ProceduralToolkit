using System.Collections;
using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Components.GeneratorSettings;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static ProceduralToolkit.EditorTests.Utils.Skips;

namespace ProceduralToolkit.EditorTests.E2E
{
    public class LandscapeGeneratorE2E
    {
        private Scene testScene;

        private const string TEST_SCENE = "Assets/Tests/Editor/E2E/Scenes/LandscapeGeneratorE2E.unity";
        private const float TEST_LENGTH = 2f;

        private GameObject Root => GameObject.Find("LandscapeGenerator");
        private GeneratorViewRoot View =>
            Root.GetComponentInChildren<GeneratorViewRoot>();

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
            if (Root != null)
            {
                Object.DestroyImmediate(Root);
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
            AssertRootCreated();
            AssertRootHasLandscapeGenerator();
            AssertViewCreated();
            AssertMeshCreatedCorrectly();
            AssertMaterialAssigned();
        }

        private void AssertRootCreated()
        {
            Assert.That(Root, Is.Not.Null);
        }

        private void AssertRootHasLandscapeGenerator()
        {
            Assert.That(Root.GetComponent<LandscapeGenerator>(), Is.Not.Null);
        }

        private void AssertViewCreated()
        {
            Assert.That(View, Is.Not.Null);
        }

        private void AssertMeshCreatedCorrectly()
        {
            var generatedMesh = View.GetComponent<MeshFilter>().sharedMesh;
            Assert.That(generatedMesh, Is.Not.Null);
            Assert.That(generatedMesh.vertexCount, Is.EqualTo(6));
        }

        private void AssertMaterialAssigned()
        {
            var material = View.GetComponent<MeshRenderer>().sharedMaterial;
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
            AssertRootEliminated();
        }

        private IEnumerator ExecuteUndo()
        {
            Undo.PerformUndo();
            yield return SkipFrames();
        }

        private void AssertRootEliminated()
        {
            Assert.That(Root == null);
        }

        [UnityTest]
        public IEnumerator TestOnPlaneSettingsUpdate()
        {
            yield return ChangeLength();
            AssertNewMeshLength();
        }

        private IEnumerator ChangeLength()
        {
            var plane = Root.GetComponent<PlaneSettings>();
            plane.length = TEST_LENGTH;
            plane.OnValidate();

            yield return SkipFrames();
        }

        private void AssertNewMeshLength()
        {
            var resultingMesh = View.GetComponent<MeshFilter>().sharedMesh;
            var vertices = resultingMesh.vertices;
            var resultingLength = Vector3.Distance(vertices[0], vertices[1]);
            Assert.That(resultingLength, Is.EqualTo(TEST_LENGTH));
        }
    }
}
