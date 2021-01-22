using System;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.EditorTests.Unit.Services.DI
{
    [Category("Unit")]
    public class CycleProtectorTests
    {
        internal interface I { }
        internal class Class1 : I { }
        internal class Class2 : I { }

        private CycleProtector protector;

        [SetUp]
        public void Setup()
        {
            protector = new CycleProtector();
        }

        [Test]
        public void TestPushDoesNotThrowExceptionOnDifferentClasses()
        {
            protector.Push(typeof(Class1));
            protector.Push(typeof(Class2));
            Assert.Pass();
        }

        [Test]
        public void TestPushThrowsExceptionOnSameClasses()
        {
            TestPush(() =>
            {
                protector.Push(typeof(Class1));
                protector.Push(typeof(Class1));
            });
        }

        [Test]
        public void TestPushThrowsExceptionOnPushingInterfaceFirst()
        {
            TestPush(() =>
            {
                protector.Push(typeof(I));
                protector.Push(typeof(Class1));
            });
        }

        [Test]
        public void TestPushThrowsExceptionOnPushingConcreteFirst()
        {
            TestPush(() =>
            {
                protector.Push(typeof(Class1));
                protector.Push(typeof(I));
            });
        }

        private void TestPush(Action pushScenario)
        {
            try
            {
                pushScenario.Invoke();
                Assert.Fail();
            }
            catch (CircularDependencyException)
            {
                Assert.Pass();
            }
        }
    }
}