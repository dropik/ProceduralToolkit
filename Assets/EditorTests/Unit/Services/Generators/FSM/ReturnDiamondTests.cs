using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnDiamondTests : BaseDiamondTilingStateTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(FSMContext context)
        {
            return new ReturnDiamond(context);
        }

        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            Context.DiamondTilingContext.XZShift = new Vector3(0.5f, 0, 0.5f);
            var expectedVertex = new Vector3(1.5f, 0, 0.5f);
            Assert.That(ReturnVertex.MoveNext(InputVertices[0]), Is.EqualTo(expectedVertex));
        }
    }
}
