using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreFirstTests : BaseStateDecoratorTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            var context = base.CreateContext(columns);
            context.DiamondTilingContext = new DiamondTilingContext();
            return context;
        }

        protected override BaseStateDecorator CreateDecorator(IState wrappee, FSMSettings settings)
        {
            return new StoreFirst(wrappee, settings);
        }

        [Test]
        public void TestFirstStored()
        {
            var expectedVertex = new Vector3(1, 2, 3);
            StateDecorator.MoveNext(expectedVertex);
            Assert.That(Settings.FSMContext.DiamondTilingContext.First, Is.EqualTo(expectedVertex));
        }
    }
}
