using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;
using System.Linq;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class DiamondSquareIT
    {
        [Test]
        public void TestDiamondSquareAlgorithm()
        {
            var input = new Vector3[]
            {
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0)
            };

            var settings = new DSASettings { Iterations = 2 };
            var ds = new DiamondSquare { Settings = settings, InputVertices = input };

            Assert.That(ds.OutputVertices.Count(), Is.EqualTo(25));
        }
    }
}
