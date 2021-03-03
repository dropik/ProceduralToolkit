using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputNewSquareTests
    {
        [Test]
        public void TestReturnedNewVertexAndOriginal()
        {
            var input = new Vector3(1, 2, 3);
            var context = new TilingContext { XZShift = new Vector3(-0.5f, 0, 0) };
            var output = new OutputNewSquare(context);
            var expectedResult = new Vector3[]
            {
                new Vector3(0.5f, 2, 3),
                input
            };

            var result = output.GetOutputFor(input);

            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}