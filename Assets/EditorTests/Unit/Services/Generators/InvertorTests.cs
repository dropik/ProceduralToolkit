using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class InvertorTests
    {
        [Test]
        public void TestVerticesAddedToInput()
        {
            var originalInput = new Vector3[15];
            var columnsInRow = 4;
            var invertor = new Invertor(default)
            {
                InputVertices = originalInput,
                ColumnsInRow = columnsInRow
            };
            Assert.That(invertor.InputVertices.Count(), Is.EqualTo(originalInput.Length + columnsInRow - 1));
        }
    }
}
