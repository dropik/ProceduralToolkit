using System.Collections;
using NUnit.Framework;
using ProceduralToolkit.Components.Generators;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static ProceduralToolkit.EditorTests.Utils.Skips;

namespace ProceduralToolkit.EditorTests.E2E
{
    [Category("E2E")]
    public class LandscapeGeneratorE2E
    {
        private Scene testScene;

        private const string TEST_SCENE = "Assets/EditorTests/E2E/Scenes/LandscapeGeneratorE2E.unity";

        private GameObject Root => GameObject.Find("LandscapeGenerator");
        private GameObject View => Root.transform.Find("view").gameObject;

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
            yield return TweakSettings();
            yield return SkipFrames();
        }

        private IEnumerator TweakSettings()
        {
            var plane = Root.GetComponent<Rectangle>();
            plane.width = 100;
            plane.length = 100;

            var ds = Root.GetComponent<DiamondSquare>();
            ds.seed = 1024;
            ds.magnitude = 15;
            ds.OnValidate();
            yield return SkipFrames();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(Root);
            EditorSceneManager.SaveScene(testScene);
        }

        [UnityTest]
        public IEnumerator TestNewLandscapeGeneratorSavedOnSceneReload()
        {
            var vertices = GetCurrentVertices();
            yield return ReloadScene();
            AssertGeneration(vertices);
        }

        private Vector3[] GetCurrentVertices()
        {
            return View.GetComponent<MeshFilter>().sharedMesh.vertices;
        }

        private IEnumerator ReloadScene()
        {
            EditorSceneManager.SaveScene(testScene);
            yield return OpenScene();
        }

        private void AssertGeneration(Vector3[] vertices)
        {
            AssertViewCreated();
            AssertMeshCreated(vertices);
            AssertMaterialAssigned();
        }

        private void AssertViewCreated()
        {
            Assert.That(View, Is.Not.Null);
        }

        private void AssertMeshCreated(Vector3[] vertices)
        {
            var generatedMesh = View.GetComponent<MeshFilter>().sharedMesh;
            Assert.That(generatedMesh, Is.Not.Null);
            CollectionAssert.AreEqual(vertices, generatedMesh.vertices);
        }

        private void AssertMaterialAssigned()
        {
            var material = View.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(material, Is.Not.Null);
        }
    }
}
