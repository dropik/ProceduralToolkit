using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputCopiesTests
    {
        [Test]
        public void TestReturnedVertices()
        {
            const int columns = 2;
            var context = new RowDuplicatorContext(columns);
            for (int i = 0; i < columns; i++)
            {
                context.VerticesCopies[i] = new Vector3(0, 0, i);
            }
            var input = new Vector3(1, 2, 3);
            var expectedVertices = new Vector3[] { input }.Concat(context.VerticesCopies);
            var output = new OutputCopies(context);

            CollectionAssert.AreEqual(expectedVertices, output.GetOutputFor(input));
        }
    }
}
