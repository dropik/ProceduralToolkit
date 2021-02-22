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
            return new DiamondTilingContext(columns);
        }

        protected override BaseDiamondTilingState GetReturnVertex(FSMContext context)
        {
            return new ReturnDiamond(context as DiamondTilingContext);
        }

        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            var context = Context as DiamondTilingContext;
            context.XZShift = new Vector3(0.5f, 0, 0.5f);
            var expectedVertex = new Vector3(1.5f, 0, 0.5f);
            Assert.That(ReturnVertex.MoveNext(InputVertices[0]), Is.EqualTo(expectedVertex));
        }
    }
}
