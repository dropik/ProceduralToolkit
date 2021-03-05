using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class RowCounterTests
    {
        [Test]
        public void TestRowIncremented()
        {
            var context = new NormalizingContext();
            var processor = new RowCounter(context);

            processor.Process(default);

            Assert.That(context.Row, Is.EqualTo(1));
        }
    }
}
