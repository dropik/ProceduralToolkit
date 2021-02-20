using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
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
