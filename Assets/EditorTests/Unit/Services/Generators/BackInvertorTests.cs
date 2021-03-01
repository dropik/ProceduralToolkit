using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class BackInvertorTests : WithInputSuffixGeneratorTests
    {
        protected override BaseVerticesGenerator CreateGenerator()
        {
            return new BackInvertor(MachineProvider);
        }

        [Test]
        public override void TestSuffixAddedToInput()
        {
            var originalInput = new Vector3[15];
            var columnsInRow = 4;
            var suffixLength = columnsInRow;
            var invertor = Generator as ColumnsBasedGenerator;
            invertor.InputVertices = originalInput;
            invertor.ColumnsInRow = columnsInRow;
            Assert.That(invertor.InputVertices.Count(), Is.EqualTo(originalInput.Length + suffixLength));
        }
    }
}
