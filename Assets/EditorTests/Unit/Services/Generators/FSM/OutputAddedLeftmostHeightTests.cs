﻿using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class OutputAddedLeftmostHeightTests
    {
        [Test]
        public void TestHeightAddedToOutput()
        {
            var input = new Vector3(1, 2, 3);
            var height = 10;
            var context = new AdderContext(3)
            {
                Column = 2
            };
            context.Heights[2] = height;
            var expectedVertex = new Vector3(input.x, input.y + height, input.z);
            var output = new OutputAddedLeftmostHeight(context);

            var result = output.GetOutputFor(input).First();

            Assert.That(result, Is.EqualTo(expectedVertex));
        }
    }
}
