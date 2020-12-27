using Moq;
using NUnit.Framework;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit
{
    public class LandscapeGeneratorTest
    {
        private LandscapeGenerator landscapeGenerator;
        private Mock<IMeshBuilder> mockMeshBuilder;
        private Mock<IMaterialProvider> mockMaterialProvider;

        private const string TEST_MESH_NAME = "Test Mesh";
        private const string TEST_MATERIAL_NAME = "Test Material";

        [SetUp]
        public void SetUp()
        {
            SetupMock();
            SetupGenerator();

        }

        private void SetupMock()
        {
            mockMeshBuilder = new Mock<IMeshBuilder>();
            mockMeshBuilder.Setup(m => m.Build()).Returns(new Mesh() { name = TEST_MESH_NAME });

            mockMaterialProvider = new Mock<IMaterialProvider>();
            mockMaterialProvider.Setup(m => m.GetMaterial()).Returns(new Material(Shader.Find("Standard")) { name = TEST_MATERIAL_NAME });
        }

        private void SetupGenerator()
        {
            landscapeGenerator = new GameObject().AddComponent<LandscapeGenerator>();
            landscapeGenerator.MeshBuilder = mockMeshBuilder.Object;
            landscapeGenerator.DefaultMaterialProvider = mockMaterialProvider.Object;
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(landscapeGenerator.gameObject);
        }

        [Test]
        public void TestMeshBuiltUsingBuilder()
        {
            landscapeGenerator.Generate();
            mockMeshBuilder.Verify(m => m.Build(), Times.Once);
        }

        [Test]
        public void TestMeshAssignedToMeshFilter()
        {
            landscapeGenerator.Generate();

            var meshFilter = landscapeGenerator.GetComponent<MeshFilter>();
            Assert.That(meshFilter.sharedMesh.name, Is.EqualTo(TEST_MESH_NAME));
        }

        [Test]
        public void TestMaterialAssignedWhenWasNotSet()
        {
            landscapeGenerator.Generate();

            mockMaterialProvider.Verify(m => m.GetMaterial(), Times.Once);
            var meshRenderer = landscapeGenerator.GetComponent<MeshRenderer>();
            Assert.That(meshRenderer.sharedMaterial.name, Is.EqualTo(TEST_MATERIAL_NAME));
        }

        [Test]
        public void TestMaterialNotModifiedWhenWasSet()
        {
            var meshRenderer = landscapeGenerator.GetComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

            landscapeGenerator.Generate();
            mockMaterialProvider.Verify(m => m.GetMaterial(), Times.Never);
        }
    }
}
