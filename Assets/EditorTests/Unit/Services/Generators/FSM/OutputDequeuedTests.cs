using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputDequeuedTests
    {
        [Test]
        public void TestOutputIsDequeuedVertex()
        {
            var context = new InvertorContext(3);
            var expected = new Vector3(1, 2, 3);
            context.Queue.Enqueue(expected);
            context.Queue.Enqueue(new Vector3(4, 5, 6));
            var output = new OutputDequeued(context);

            var result = output.GetOutputFor(default);

            Assert.That(result.First(), Is.EqualTo(expected));
        }
    }
}
