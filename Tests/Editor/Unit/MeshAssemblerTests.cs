using Moq;
using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit.EditorTests.Unit
{
    public class MeshAssemblerTests
    {
        private MeshAssembler meshAssembler;
        private Mock<IMeshBuilder> mockMeshBuilder;
        private Mock<IGeneratorView> mockMeshGeneratorView;
        private Mock<IGeneratorView> mockMaterialGeneratorView;

        private const string TEST_MESH_NAME = "Test Mesh";

        [SetUp]
        public void SetUp()
        {
            SetupMock();
            SetupGenerator();
        }

        private void SetupMock()
        {
            mockMeshBuilder = new Mock<IMeshBuilder>();
            mockMeshBuilder.Setup(m => m.Build())
                           .Returns(new Mesh() { name = TEST_MESH_NAME });

            mockMeshGeneratorView = new Mock<IGeneratorView>();
            mockMaterialGeneratorView = new Mock<IGeneratorView>();
        }

        private void SetupGenerator()
        {
            meshAssembler = new MeshAssembler(
                mockMeshBuilder.Object,
                mockMeshGeneratorView.Object,
                mockMaterialGeneratorView.Object
            );
        }

        [Test]
        public void TestMeshBuiltUsingBuilder()
        {
            meshAssembler.Assemble();
            mockMeshBuilder.Verify(m => m.Build(), Times.Once);
        }

        [Test]
        public void TestMeshOnGenerateCalled()
        {
            meshAssembler.Assemble();

            mockMeshGeneratorView.Verify(
                m => m.OnGenerate(It.Is<Mesh>(mesh => mesh.name == TEST_MESH_NAME)),
                Times.Once
            );
        }

        [Test]
        public void TestMaterialOnGenerateCalled()
        {
            meshAssembler.Assemble();

            mockMaterialGeneratorView.Verify(
                m => m.OnGenerate(It.IsAny<Mesh>()),
                Times.Once
            );
        }
    }
}
