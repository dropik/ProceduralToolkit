using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
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