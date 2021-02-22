using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreFirstTests : ReturnOriginalTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            return new DiamondTilingContext(columns);
        }

        protected override BaseDiamondTilingState GetReturnVertex(FSMContext context)
        {
            return new StoreFirst(context as DiamondTilingContext);
        }

        [Test]
        public void TestFirstStored()
        {
            var context = Context as DiamondTilingContext;
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(context.First, Is.EqualTo(InputVertices[0]));
        }
    }
}
