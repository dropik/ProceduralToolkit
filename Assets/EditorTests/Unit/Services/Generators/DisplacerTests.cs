using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DisplacerTests
    {
        private readonly Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 1)
        };
        private float GetDisplacement(float magnitude) => Random.Range(-magnitude, magnitude);

        private DSASettings settings;
        private Displacer displacer;

        private const int TEST_SEED = 0;
        private const float TEST_MAGNITUDE = 2;


        [SetUp]
        public void Setup()
        {
            settings = new DSASettings()
            {
                Seed = TEST_SEED,
                Magnitude = TEST_MAGNITUDE
            };
            Random.InitState(TEST_SEED);
            displacer = new Displacer()
            {
                InputVertices = vertices,
                Settings = settings
            };
        }

        [Test]
        public void TestOnZeroIteration()
        {
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1 + GetDisplacement(TEST_MAGNITUDE), 0),
                new Vector3(1, 1 + GetDisplacement(TEST_MAGNITUDE), 0),
                new Vector3(0, 1 + GetDisplacement(TEST_MAGNITUDE), 1)
            };
            displacer.Iteration = 0;

            CollectionAssert.AreEqual(expectedVertices, displacer.OutputVertices);
        }

        [Test]
        public void TestOnNonZeroIteration()
        {
            settings.Hardness = 0.5f;
            var magnitude = TEST_MAGNITUDE * 0.5f;
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1 + GetDisplacement(magnitude), 0),
                new Vector3(1, 1 + GetDisplacement(magnitude), 0),
                new Vector3(0, 1 + GetDisplacement(magnitude), 1)
            };
            displacer.Iteration = 2;

            CollectionAssert.AreEqual(expectedVertices, displacer.OutputVertices);
        }

        [Test]
        public void TestWithMask()
        {
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1 + GetDisplacement(TEST_MAGNITUDE), 0),
                new Vector3(1, 1, 0),
                new Vector3(0, 1 + GetDisplacement(TEST_MAGNITUDE), 1)
            };
            displacer.Iteration = 0;
            displacer.Mask = new bool[] { true, false, true };

            CollectionAssert.AreEqual(expectedVertices, displacer.OutputVertices);
        }

        [Test]
        public void TestOnNoInutProvided()
        {
            displacer = new Displacer()
            {
                Settings = new DSASettings()
            };
            CollectionAssert.AreEqual(new Vector3[0], displacer.OutputVertices);
        }

        [Test]
        public void TestOnNoSettingsProvided()
        {
            displacer = new Displacer()
            {
                InputVertices = new Vector3[14]
            };
            _ = displacer.OutputVertices;
        }
    }
}