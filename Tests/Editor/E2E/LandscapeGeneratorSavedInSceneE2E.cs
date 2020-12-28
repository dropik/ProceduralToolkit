using System.Collections;
using NUnit.Framework;
using ProceduralToolkit.EditorTests.Utils;
using ProceduralToolkit.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.E2E
{
    public class LandscapeGeneratorSavedInSceneE2E
    {
        private NewLandscapeGeneratorWindow window;
        private Scene testScene;

        private const string TEST_SCENE = "Assets/ProceduralToolkit/Tests/Editor/E2E/Scenes/LandscapeGeneratorSavedInSceneE2E.unity";

        private GeneratorBoot Boot =>
            Object.FindObjectOfType<GeneratorBoot>();

        [SetUp]
        public void SetUp()
        {
            NewLandscapeGeneratorWindow.ShowWindow();
            window = EditorWindow.GetWindow<NewLandscapeGeneratorWindow>();

            testScene = EditorSceneManager.OpenScene(TEST_SCENE);
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
            if (Boot != null)
            {
                Object.DestroyImmediate(Boot.gameObject);
                EditorSceneManager.SaveScene(testScene);
            }
        }

        [UnityTest]
        public IEnumerator TestLandscapeGeneratorSavedOnSceneReload()
        {
            yield return Arrange();
            ReloadScene();
            AssertSceneSaved();
        }

        private IEnumerator Arrange()
        {
            yield return SetupWindow();
            yield return ClickCreateGeneratorButton();
        }

        private IEnumerator SetupWindow()
        {
            var baseShape = Resources.Load<PlaneGeneratorSettings>("TestPlaneSettings");
            window.baseShape = baseShape;
            yield return SkipFrames();
        }

        private IEnumerator ClickCreateGeneratorButton()
        {
            var buttonClicker = new ButtonClicker();
            var createButton = window.rootVisualElement.Query<Button>("createGenerator").First();
            buttonClicker.Click(createButton);
            yield return SkipFrames();
        }

        private void ReloadScene()
        {
            EditorSceneManager.SaveScene(testScene);
            testScene = EditorSceneManager.OpenScene(TEST_SCENE);
        }

        private void AssertSceneSaved()
        {
            Assert.That(Boot != null);
            var landscapeGenerator = Boot.GetComponentInChildren<LandscapeGenerator>();
            Assert.That(landscapeGenerator != null);
            Assert.That(landscapeGenerator.GetComponent<MeshFilter>().sharedMesh != null);
            Assert.That(landscapeGenerator.GetComponent<MeshRenderer>().sharedMaterial != null);
        }
    }
}
