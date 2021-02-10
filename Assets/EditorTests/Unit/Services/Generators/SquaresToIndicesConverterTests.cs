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
                new Square(0, 1, 2, 3)
            };
            var expectedIndices = new int[]
            {
                0, 1, 2,
                0, 2, 3
            };
            var converter = new SquaresToIndicesConverter(squares);
            CollectionAssert.AreEqual(expectedIndices, converter.Indices);
        }
    }
}
