using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Components.Startups;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class LandscapeGeneratorIT
    {
        internal class LandscapeGeneratorWithMockMeshAssembler : LandscapeGenerator
        {
            protected override void SetupMeshAssemblerServices(IServiceContainer services)
            {
                var mockMeshAssembler = new Mock<IMeshAssembler>();
                services.AddSingleton(mockMeshAssembler.Object);
            }
        }

        private GameObject gameObject;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestRegisterUndo()
        {
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            landscapeGenerator.RegisterUndo();
            Undo.PerformUndo();
            Assert.That(gameObject == null);
        }

        [Test]
        public void TestViewInitialized()
        {
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            var view = gameObject.transform.Find("view");
            Assert.That(view != null);
            var meshRenderer = view.GetComponent<MeshRenderer>();
            Assert.That(meshRenderer != null);
            Assert.That(meshRenderer.sharedMaterial != null);
            Assert.That(view.GetComponent<MeshGeneratorView>() != null);
        }
    }
}
