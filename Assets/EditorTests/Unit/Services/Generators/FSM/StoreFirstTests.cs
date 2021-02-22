using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreFirstTests : ReturnOriginalTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(FSMContext context)
        {
            return new StoreFirst(context);
        }

        [Test]
        public void TestFirstStored()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Context.DiamondTilingContext.First, Is.EqualTo(InputVertices[0]));
        }
    }
}
