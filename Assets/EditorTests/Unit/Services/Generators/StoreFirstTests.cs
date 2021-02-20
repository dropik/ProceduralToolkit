using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class StoreFirstTests : ReturnOriginalTests
    {
        [Test]
        public void TestFirstStored()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Context.First, Is.EqualTo(InputVertices[0]));
        }

        protected override BaseDiamondTilingState GetReturnVertex(DiamondTilingContext context)
        {
            return new StoreFirst(context);
        }
    }
}
