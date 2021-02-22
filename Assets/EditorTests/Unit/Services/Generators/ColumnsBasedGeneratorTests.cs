using NUnit.Framework;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    public abstract class ColumnsBasedGeneratorTests : BaseVerticesGeneratorTests
    {
        [Test]
        public void TestOnNegativeColumnsInRow()
        {
            var columnsBasedGenerator = Generator as ColumnsBasedGenerator;
            columnsBasedGenerator.ColumnsInRow = -2;
            Assert.That(columnsBasedGenerator.ColumnsInRow, Is.Zero);
        }
    }
}
