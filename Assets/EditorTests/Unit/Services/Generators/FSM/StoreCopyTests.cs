using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreCopyTests : BaseStateDecoratorTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            var context = base.CreateContext(columns);
            context.RowDuplicatorContext = new RowDuplicatorContext(columns);
            return context;
        }

        protected override BaseStateDecorator CreateDecorator(IStateBehaviour wrappee, FSMSettings settings)
        {
            return new StoreCopy(wrappee, settings);
        }

        [Test]
        public void TestVertexCopyStored()
        {
            var expectedVertex = new Vector3(1, 2, 3);
            StateDecorator.MoveNext(expectedVertex);
            Assert.That(Settings.FSMContext.RowDuplicatorContext.VerticesCopies[0], Is.EqualTo(expectedVertex));
        }
    }
}
