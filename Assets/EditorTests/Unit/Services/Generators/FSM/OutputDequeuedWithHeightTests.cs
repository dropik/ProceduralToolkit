﻿using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputDequeuedWithHeightTests
    {
        [Test]
        public void TestOutputIsFromQueueAndWithHeight()
        {
            var enqueue = new Vector3(1, 2, 3);
            var input = new Vector3(4, 5, 6);
            var expected = new Vector3(1, 7, 3);
            var context = new InvertorContext(1);
            context.Queue.Enqueue(enqueue);
            var output = new OutputDequeuedWithHeight(context);

            var result = output.GetOutputFor(input).First();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}