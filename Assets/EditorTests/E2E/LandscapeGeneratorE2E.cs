using NUnit.Framework;
using ProceduralToolkit.Components.GeneratorSettings;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static ProceduralToolkit.Utils.Skips;

namespace ProceduralToolkit.E2E
{
    [Category("E2E")]
    public class LandscapeGeneratorE2E
    {
        private Scene testScene;

        private const string TEST_SCENE = "Assets/EditorTests/E2E/Scenes/LandscapeGeneratorE2E.unity";

        private GameObject Root => GameObject.Find("Landscape Generator");
        private TerrainData TerrainData => Root.GetComponent<Terrain>().terrainData;

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
            EditorApplication.QueuePlayerLoopUpdate();
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
            var ds = Root.GetComponent<DiamondSquare>();
            ds.settings.seed = 10;
            ds.OnValidate();
            EditorApplication.QueuePlayerLoopUpdate();
            yield return SkipFrames();

            var resolution = TerrainData.heightmapResolution;
            var heights1 = TerrainData.GetHeights(0, 0, resolution, resolution);

            EditorSceneManager.SaveScene(testScene);
            yield return OpenScene();
            EditorApplication.QueuePlayerLoopUpdate();
            yield return SkipFrames();

            var heights2 = TerrainData.GetHeights(0, 0, resolution, resolution);
            CollectionAssert.AreEqual(heights1, heights2);
            Assert.That(Root.GetComponent<Terrain>().materialTemplate, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator TestMeshUpdatedOnDsaSettingsChange()
        {
            var terrain = Root.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            var heights1 = terrainData.GetHeights(0, 0, 33, 33);
            
            var ds = Root.GetComponent<DiamondSquare>();
            ds.settings.seed = 10;
            ds.OnValidate();
            EditorApplication.QueuePlayerLoopUpdate();
            yield return SkipFrames();

            var heights2 = terrainData.GetHeights(0, 0, 33, 33);
            CollectionAssert.AreNotEqual(heights1, heights2);
        }
    }
}
