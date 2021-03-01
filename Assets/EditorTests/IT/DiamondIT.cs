using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class DiamondIT
    {
        [Test]
        public void TestDiamond()
        {
            var row1 = new Vector3[]
            {
                new Vector3(0, 5, 4),
                new Vector3(1, 7, 4),
                new Vector3(2, 21, 4),
                new Vector3(3, 6, 4),
                new Vector3(4, 87, 4)
            };
            var row2 = new Vector3[]
            {
                new Vector3(0, 3, 3),
                new Vector3(1, 5, 3),
                new Vector3(2, 8, 3),
                new Vector3(3, 33, 3),
                new Vector3(4, 77, 3)
            };
            var row3 = new Vector3[]
            {
                new Vector3(0, 3, 2),
                new Vector3(1, 7, 2),
                new Vector3(2, 76, 2),
                new Vector3(3, 7, 2),
                new Vector3(4, 4, 2)
            };
            var row4 = new Vector3[]
            {
                new Vector3(0, 83, 1),
                new Vector3(1, 2, 1),
                new Vector3(2, 45, 1),
                new Vector3(3, 71, 1),
                new Vector3(4, 52, 1)
            };
            var row5 = new Vector3[]
            {
                new Vector3(0, 13, 0),
                new Vector3(1, 4, 0),
                new Vector3(2, 6, 0),
                new Vector3(3, 27, 0),
                new Vector3(4, 17, 0)
            };
            var input = row1.Concat(row2).Concat(row3).Concat(row4).Concat(row5);

            var d = new Vector3(0.5f, 0, -0.5f);
            Random.InitState(0);
            Vector3 Displacement() => new Vector3(0, Random.Range(-1f, 1f), 0);
            

            var expectedDiamond1 = new Vector3[]
            {
                new Vector3(0, 5, 4) + d + Displacement(),
                new Vector3(1, 10.25f, 4) + d + Displacement(),
                new Vector3(2, 17, 4) + d + Displacement(),
                new Vector3(3, 50.75f, 4) + d + Displacement()
            };
            var expectedDiamond2 = new Vector3[]
            {
                new Vector3(0, 4.5f, 3) + d + Displacement(),
                new Vector3(1, 24, 3) + d + Displacement(),
                new Vector3(2, 31, 3) + d + Displacement(),
                new Vector3(3, 30.25f, 3) + d + Displacement()
            };
            var expectedDiamond3 = new Vector3[]
            {
                new Vector3(0, 23.75f, 2) + d + Displacement(),
                new Vector3(1, 32.5f, 2) + d + Displacement(),
                new Vector3(2, 49.75f, 2) + d + Displacement(),
                new Vector3(3, 33.5f, 2) + d + Displacement()
            };
            var expectedDiamond4 = new Vector3[]
            {
                new Vector3(0, 25.5f, 1) + d + Displacement(),
                new Vector3(1, 14.25f, 1) + d + Displacement(),
                new Vector3(2, 37.25f, 1) + d + Displacement(),
                new Vector3(3, 41.75f, 1) + d + Displacement()
            };
            var expected = row1
                           .Concat(expectedDiamond1)
                           .Concat(row2)
                           .Concat(expectedDiamond2)
                           .Concat(row3)
                           .Concat(expectedDiamond3)
                           .Concat(row4)
                           .Concat(expectedDiamond4)
                           .Concat(row5);

            var diamond = new Diamond()
            {
                Settings = new DSASettings()
                {
                    Seed = 0,
                    Hardness = 0,
                    Magnitude = 1
                },
                InputVertices = input,
                Iteration = 2
            };

            CollectionAssert.AreEqual(expected, diamond.OutputVertices);
        }
    }
}