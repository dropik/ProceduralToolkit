using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnDiamondTests : BaseDiamondTilingStateTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            return new FSMContext(columns)
            {
                DiamondTilingContext = new DiamondTilingContext()
            };
        }

        protected override BaseDiamondTilingState GetReturnVertex(FSMContext context)
        {
            return new ReturnDiamond(context);
        }

        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            Context.DiamondTilingContext.XZShift = new Vector3(0.5f, 0, 0.5f);
            var expectedVertices = new Vector3[] { new Vector3(1.5f, 0, 0.5f) };
            CollectionAssert.AreEqual(expectedVertices, ReturnVertex.MoveNext(InputVertices[0]));
        }
    }
}
