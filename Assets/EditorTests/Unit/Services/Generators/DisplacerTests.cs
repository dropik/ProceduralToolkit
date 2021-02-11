using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DisplacerTests
    {
        private Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 1)
        };
        private DSASettings settings;
        private System.Func<float, float> elevator = (magnitude) => Random.Range(-magnitude, magnitude);

        private const int TEST_SEED = 0;
        private const float TEST_MAGNITUDE = 1;


        [SetUp]
        public void Setup()
        {
            settings = new DSASettings()
            {
                Seed = TEST_SEED,
                Magnitude = TEST_MAGNITUDE
            };
            Random.InitState(TEST_SEED);
        }

        [Test]
        public void TestOnZeroIteration()
        {
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1 + elevator.Invoke(TEST_MAGNITUDE), 0),
                new Vector3(1, 1 + elevator.Invoke(TEST_MAGNITUDE), 0),
                new Vector3(0, 1 + elevator.Invoke(TEST_MAGNITUDE), 1)
            };
            var displacer = new Displacer(vertices, settings, 0);
            CollectionAssert.AreEqual(expectedVertices, displacer.Vertices);
        }

        [Test]
        public void TestOnNonZeroIteration()
        {
            settings.Hardness = 0.5f;
            var magnitude = TEST_MAGNITUDE * 0.5f;
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1 + elevator.Invoke(magnitude), 0),
                new Vector3(1, 1 + elevator.Invoke(magnitude), 0),
                new Vector3(0, 1 + elevator.Invoke(magnitude), 1)
            };
            var displacer = new Displacer(vertices, settings, 2);
            CollectionAssert.AreEqual(expectedVertices, displacer.Vertices);
        }

        [Test]
        public void TestWithMask()
        {
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1 + elevator.Invoke(TEST_MAGNITUDE), 0),
                new Vector3(1, 1, 0),
                new Vector3(0, 1 + elevator.Invoke(TEST_MAGNITUDE), 1)
            };
            var displacer = new Displacer(vertices, settings, 0)
            {
                Mask = new bool[] { true, false, true }
            };
            CollectionAssert.AreEqual(expectedVertices, displacer.Vertices);
        }
    }
}