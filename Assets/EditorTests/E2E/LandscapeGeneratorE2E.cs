using System.Collections;
using NUnit.Framework;
using ProceduralToolkit.Components.Startups;
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
        private const float TEST_LENGTH = 2f;

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

        private void AssertGeneration()
        {
            AssertViewCreated();
            AssertMeshCreated();
            AssertMaterialAssigned();
        }

        private void AssertViewCreated()
        {
            Assert.That(View, Is.Not.Null);
        }

        private void AssertMeshCreated()
        {
            var generatedMesh = View.GetComponent<MeshFilter>().sharedMesh;
            Assert.That(generatedMesh, Is.Not.Null);
            Assert.That(generatedMesh.vertexCount, Is.GreaterThan(0));
        }

        private void AssertMaterialAssigned()
        {
            var material = View.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(material, Is.Not.Null);
        }
    }
}
