using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnCopiesTests : BaseStateDecoratorTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            var context = base.CreateContext(columns);
            context.RowDuplicatorContext = new RowDuplicatorContext(columns);
            return context;
        }

        protected override BaseStateDecorator CreateDecorator(IStateBehaviour wrappee, FSMSettings settings)
        {
            return new ReturnCopies(wrappee, settings);
        }

        [Test]
        public void TestReturnedVertices()
        {
            var context = Settings.FSMContext;
            context.Column = 1;
            for (int i = 0; i < context.ColumnsInRow; i++)
            {
                context.RowDuplicatorContext.VerticesCopies[i] = new Vector3(0, 0, i);
            }
            var input = new Vector3(1, 2, 3);
            var expectedVertices = new Vector3[] { input }.Concat(context.RowDuplicatorContext.VerticesCopies);

            CollectionAssert.AreEqual(expectedVertices, StateDecorator.MoveNext(input));
        }
    }
}
