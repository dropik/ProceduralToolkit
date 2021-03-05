using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class RectangleGeneratorTests
    {
        private RectangleGenerator rect;

        [SetUp]
        public void SetUp()
        {
            rect = new RectangleGenerator(new RectangleGeneratorSettings()
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
                new Vector3(1, 0, -0.5f)
            };
            CollectionAssert.AreEqual(expectedVertices, rect.Vertices);
        }

        [Test]
        public void TestForCorrectSquares()
        {
            var expectedSquares = new Models.Square[]
            {
                new Models.Square(0, 1, 3, 2)
            };
            CollectionAssert.AreEqual(expectedSquares, rect.Squares);
        }
    }
}
