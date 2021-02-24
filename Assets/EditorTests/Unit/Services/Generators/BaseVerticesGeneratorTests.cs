using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    public abstract class BaseVerticesGeneratorTests
    {
        protected BaseVerticesGenerator Generator { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            Generator = CreateGenerator();
        }

        protected abstract Func<IEnumerable<Vector3>, int, FSMContext> ContextProvider { get; }

        protected abstract BaseVerticesGenerator CreateGenerator();

        [Test]
        public void TestOnInputVerticesNotSet()
        {
            var expectedVertices = new Vector3[0];
            CollectionAssert.AreEqual(expectedVertices, Generator.InputVertices);
        }
    }
}
