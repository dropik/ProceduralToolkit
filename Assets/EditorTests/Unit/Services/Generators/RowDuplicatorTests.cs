﻿using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class RowDuplicatorTests : BaseDiamondGeneratorTests
    {
        private Mock<IRowDuplicatorState> mockState;
        private FSMContext context;

        protected override Func<IEnumerable<Vector3>, int, FSMContext> ContextProvider => (vertices, columns) => context;

        protected override BaseDiamondGenerator CreateGenerator()
        {
            return new RowDuplicator(ContextProvider);
        }

        [SetUp]
        public override void Setup()
        {
            mockState = new Mock<IRowDuplicatorState>();
            context = new FSMContext(2)
            {
                RowDuplicatorContext = new RowDuplicatorContext(2)
                {
                    State = mockState.Object
                }
            };
            base.Setup();
        }

        [Test]
        public void TestStateMoveNextUsed()
        {
            var enumerator = Generator.OutputVertices.GetEnumerator();
            enumerator.MoveNext();
            mockState.Verify(m => m.MoveNext(), Times.Once);
        }

        [Test]
        public void TestEnumerableFinishesWhenStateMoveNextReturnsFalse()
        {
            mockState.Setup(m => m.MoveNext()).Returns(false);
            var enumerator = Generator.OutputVertices.GetEnumerator();
            Assert.That(enumerator.MoveNext(), Is.False);
        }

        [Test]
        public void TestEnumerableReturnsContextCurrent()
        {
            mockState.Setup(m => m.MoveNext()).Returns(true);
            var testVertex = new Vector3(1, 2, 3);
            context.RowDuplicatorContext.Current = testVertex;
            var enumerator = Generator.OutputVertices.GetEnumerator();

            enumerator.MoveNext();

            Assert.That(enumerator.Current, Is.EqualTo(testVertex));
        }
    }
}
