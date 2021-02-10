using System.Collections.Generic;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class SquaresToIndicesConverterTests
    {
        [Test]
        public void TestConvertingOneSquare()
        {
            var squares = new Square[]
            {
                new Square(0, 1, 3, 2)
            };
            var expectedIndices = new int[]
            {
                0, 1, 3,
                0, 3, 2
            };
            TestConverter(squares, expectedIndices);
        }

        [Test]
        public void TestConvertingMultipleSquares()
        {
            var squares = new Square[]
            {
                new Square(0, 1, 4, 3),
                new Square(1, 2, 5, 4),
                new Square(3, 4, 7, 6),
                new Square(4, 5, 8, 7)
            };
            var expectedIndices = new int[]
            {
                0, 1, 4,
                0, 4, 3,
                1, 2, 5,
                1, 5, 4,
                3, 4, 7,
                3, 7, 6,
                4, 5, 8,
                4, 8, 7
            };
            TestConverter(squares, expectedIndices);
        }

        private void TestConverter(IEnumerable<Square> squares, IEnumerable<int> expectedIndices)
        {
            var converter = new SquaresToIndicesConverter(squares);
            CollectionAssert.AreEqual(expectedIndices, converter.Indices);
        }
    }
}
