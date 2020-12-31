using Moq;
using NUnit.Framework;
using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit
{
    public class MaterialGeneratorViewTests
    {
        private Mock<IMaterialContainer> mockMaterialContainer;
        private Material defaultMaterial;
        private MaterialGeneratorView view;

        private const string TEST_MATERIAL_NAME = "Test Material";

        [SetUp]
        public void SetUp()
        {
            mockMaterialContainer = new Mock<IMaterialContainer>();
            defaultMaterial = new Material(Shader.Find("Standard"))
            {
                name = TEST_MATERIAL_NAME
            };
            view = new MaterialGeneratorView(mockMaterialContainer.Object, defaultMaterial);
        }

        [Test]
        public void TestMaterialAssignedWhenWasNotSet()
        {
            mockMaterialContainer.SetupGet(m => m.Material).Returns(null as Material);

            view.OnGenerate(new Mesh());

            mockMaterialContainer.VerifySet(
                m => m.Material = It.Is<Material>(material => material.name == TEST_MATERIAL_NAME),
                Times.Once
            );
        }

        [Test]
        public void TestMaterialNotModifiedWhenWasSet()
        {
            mockMaterialContainer.SetupGet(m => m.Material)
                                 .Returns(defaultMaterial);

            view.OnGenerate(new Mesh());

            mockMaterialContainer.VerifySet(m => m.Material = It.IsAny<Material>(), Times.Never);
        }
    }
}
