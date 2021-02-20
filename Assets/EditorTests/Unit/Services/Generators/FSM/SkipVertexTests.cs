using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class SkipVertexTests : BaseDiamondTilingStateTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(DiamondTilingContext context)
        {
            return new SkipVertex(context);
        }

        [Test]
        public void TestMoveNextReturnsNull()
        {
            Assert.That(ReturnVertex.MoveNext(InputVertices[0]), Is.Null);
        }
    }
}