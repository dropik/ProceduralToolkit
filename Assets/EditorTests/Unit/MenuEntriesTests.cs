using NUnit.Framework;
using ProceduralToolkit.Components.Startups;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit
{
    [Category("Unit")]
    public class MenuEntriesTests
    {
        internal class TestStartup : Startup
        {
            public bool RegisterUndoCalled { get; private set; }

            public override void RegisterUndo()
            {
                RegisterUndoCalled = true;
            }
        }

        [Test]
        public void TestCreate()
        {
            MenuEntries.Create<TestStartup>();
            var startup = Object.FindObjectOfType<TestStartup>();
            Assert.That(startup.RegisterUndoCalled);
        }
    }
}
