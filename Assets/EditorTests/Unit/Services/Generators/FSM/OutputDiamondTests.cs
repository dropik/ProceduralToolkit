using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputDiamondTests
    {
        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            var context = new DiamondTilingContext();
            var output = new OutputDiamond(context);
            context.XZShift = new Vector3(0.5f, 0, 0.5f);
            var input = new Vector3(1, 0, 0);
            var expectedVertices = new Vector3[] { new Vector3(1.5f, 0, 0.5f) };

            CollectionAssert.AreEqual(expectedVertices, output.GetOutputFor(input));
        }
    }
}
