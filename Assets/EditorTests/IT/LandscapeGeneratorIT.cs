using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Components.Generators;
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
            public Mock<IMeshAssembler> MockMeshAssembler { get; private set; } = new Mock<IMeshAssembler>();
            
            protected override void SetupMeshAssemblerServices(IServiceContainer services)
            {
                services.AddSingleton(MockMeshAssembler.Object);
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
            gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            var view = gameObject.transform.Find("view");
            Assert.That(view != null);
            var meshRenderer = view.GetComponent<MeshRenderer>();
            Assert.That(meshRenderer != null);
            Assert.That(meshRenderer.sharedMaterial != null);
            Assert.That(view.GetComponent<MeshGeneratorView>() != null);
        }

        [Test]
        public void TestMockAssemblerCalled()
        {
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            var assembler = gameObject.GetComponent<MeshAssemblerComponent>();
            assembler.Start();
            landscapeGenerator.MockMeshAssembler.Verify(m => m.Assemble(), Times.Once);
        }

        [Test]
        public void TestMeshUpdatedOnPlaneChange()
        {
            gameObject.AddComponent<LandscapeGenerator>();
            var assembler = gameObject.GetComponent<MeshAssemblerComponent>();
            assembler.Start();
            var view = gameObject.GetComponentInChildren<MeshGeneratorView>();
            view.Update();
            var meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
            var vertices1 = meshFilter.sharedMesh.vertices;
            var rect = gameObject.GetComponent<Rectangle>();
            rect.length = 10;
            rect.OnValidate();
            view.Update();
            var vertices2 = meshFilter.sharedMesh.vertices;
            CollectionAssert.AreNotEqual(vertices1, vertices2);
        }
        
        [Test]
        public void TestMeshUpdatedOnDiamondSquareChange()
        {
            gameObject.AddComponent<LandscapeGenerator>();
            var assembler = gameObject.GetComponent<MeshAssemblerComponent>();
            assembler.Start();
            var view = gameObject.GetComponentInChildren<MeshGeneratorView>();
            view.Update();
            var meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
            var vertices1 = meshFilter.sharedMesh.vertices;
            var ds = gameObject.GetComponent<DiamondSquare>();
            ds.magnitude = 10;
            ds.OnValidate();
            view.Update();
            var vertices2 = meshFilter.sharedMesh.vertices;
            CollectionAssert.AreNotEqual(vertices1, vertices2);
        }
    }
}
