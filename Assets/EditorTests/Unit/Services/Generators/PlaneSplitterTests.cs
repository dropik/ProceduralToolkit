using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class PlaneSplitterTests
    {
        private Mock<IGenerator> mockBaseGenerator;
        private PlaneSplitter splitter;

        [SetUp]
        public void Setup()
        {
            mockBaseGenerator = new Mock<IGenerator>();
        }

        [Test]
        public void TestVerticesAndTrianglesReceivedFromWrappee()
        {
            var testVertices = new Vector3[]
            {
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1)
            };
            var testTriangles = new int[] { 0, 1, 2 };
            mockBaseGenerator.Setup(m => m.Vertices).Returns(testVertices);
            mockBaseGenerator.Setup(m => m.Triangles).Returns(testTriangles);
            splitter = new PlaneSplitter(mockBaseGenerator.Object, new PlaneSplitterSettings()
            {
                Divisions = 0
            });

            CollectionAssert.AreEqual(testVertices, splitter.Vertices);
            CollectionAssert.AreEqual(testTriangles, splitter.Triangles);
        }
    }
}