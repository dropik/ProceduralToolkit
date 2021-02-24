using NUnit.Framework;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputOriginalTests
    {
        [Test]
        public void TestMoveNextReturnsCurrent()
        {
            var input = new Vector3(1, 2, 3);
            var expectedVertices = new Vector3[] { input };
            var output = new OutputOriginal();
            CollectionAssert.AreEqual(expectedVertices, output.GetOutputFor(input));
        }
    }
}
