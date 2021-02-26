using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputCopyTests
    {
        [Test]
        public void TestOutputIsCopy()
        {
            var context = new RowDuplicatorContext(3);
            var copies = new Vector3[]
            {
                new Vector3(1, 2, 3),
                new Vector3(4, 5, 6),
                new Vector3(7, 8 ,9)
            };
            context.Column = 2;
            var output = new OutputCopy(context);

            var result = output.GetOutputFor(default);

            Assert.That(result.First(), Is.EqualTo(context.VerticesCopies[2]));
        }
    }
}
