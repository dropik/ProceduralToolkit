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
            var context = base.CreateContext(columns);
            context.DiamondTilingContext = new DiamondTilingContext();
            return context;
        }

        protected override BaseState GetReturnVertex(FSMSettings settings)
        {
            return new StoreFirst(settings);
        }

        [Test]
        public void TestFirstStored()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Settings.FSMContext.DiamondTilingContext.First, Is.EqualTo(InputVertices[0]));
        }
    }
}
