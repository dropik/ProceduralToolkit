using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class CalculateXZShiftTests
    {
        private DiamondTilingContext context;
        private CalculateXZShift processor;

        [SetUp]
        public void Setup()
        {
            context = new DiamondTilingContext();
            processor = new CalculateXZShift(context);
        }

        [Test]
        public void TestShiftCalculated()
        {
            context.First = Vector3.zero;
            var input = new Vector3(1, 0, 0);
            var expectedShift = new Vector3(-1, 0, 0) / 2;

            processor.Process(input);

            Assert.That(context.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestShiftIsNotCalculatedWhenItIsNotZero()
        {
            var expectedShift = new Vector3(4, 4, 4);
            context.XZShift = expectedShift;

            processor.Process(default);

            Assert.That(context.XZShift, Is.EqualTo(expectedShift));
        }
    }
}