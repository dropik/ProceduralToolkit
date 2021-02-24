using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnDiamondTests : BaseStateDecoratorTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            var context = base.CreateContext(columns);
            context.DiamondTilingContext = new DiamondTilingContext();
            return context;
        }

        protected override BaseStateDecorator CreateDecorator(IStateBehaviour wrappee, FSMSettings settings)
        {
            return new ReturnDiamond(wrappee, settings);
        }

        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            Settings.FSMContext.DiamondTilingContext.XZShift = new Vector3(0.5f, 0, 0.5f);
            var input = new Vector3(1, 0, 0);
            var expectedVertices = new Vector3[] { new Vector3(1.5f, 0, 0.5f) };
            CollectionAssert.AreEqual(expectedVertices, StateDecorator.MoveNext(input));
        }
    }
}
