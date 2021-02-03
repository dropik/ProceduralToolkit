using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class PlaneGeneratorTests
    {
        private PlaneGenerator plane;

        [SetUp]
        public void SetUp()
        {
            plane = new PlaneGenerator(new PlaneGeneratorSettings()
            {
                Length = 2,
                Width = 1
            });
        }

        [Test]
        public void TestForCorrectVertices()
        {
            var expectedVertices = new Vector3[]
            {
                new Vector3(-1, 0, 0.5f),
                new Vector3(1, 0, 0.5f),
                new Vector3(-1, 0, -0.5f),
                new Vector3(-1, 0, -0.5f),
                new Vector3(1, 0, 0.5f),
                new Vector3(1, 0, -0.5f)
            };
            CollectionAssert.AreEqual(expectedVertices, plane.Vertices);
        }

        [Test]
        public void TestForCorrectTriangles()
        {
            var expectedTriangles = new int[] { 0, 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(expectedTriangles, plane.Triangles);
        }
    }
}
