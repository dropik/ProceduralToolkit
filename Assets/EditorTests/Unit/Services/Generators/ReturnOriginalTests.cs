using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnOriginalTests : BaseDiamondTilingStateTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(DiamondTilingContext context)
        {
            return new ReturnOriginal(context);
        }

        [Test]
        public void TestMoveNextReturnsCurrent()
        {
            Assert.That(ReturnVertex.MoveNext(InputVertices[0]), Is.EqualTo(InputVertices[0]));
        }
    }
}