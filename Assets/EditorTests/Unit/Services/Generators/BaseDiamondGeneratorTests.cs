using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    public abstract class BaseDiamondGeneratorTests
    {
        protected BaseDiamondGenerator Generator { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Generator = CreateGenerator();
        }

        protected abstract Func<IEnumerable<Vector3>, int, FSMContext> ContextProvider { get; }

        protected abstract BaseDiamondGenerator CreateGenerator();

        [Test]
        public void TestOnInputVerticesNotSet()
        {
            var expectedVertices = new Vector3[0];
            CollectionAssert.AreEqual(expectedVertices, Generator.InputVertices);
        }

        [Test]
        public void TestOnNegativeColumnsInRow()
        {
            Generator.ColumnsInRow = -2;
            Assert.That(Generator.ColumnsInRow, Is.Zero);
        }
    }
}
