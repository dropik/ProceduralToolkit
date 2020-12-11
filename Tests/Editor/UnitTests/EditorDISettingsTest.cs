using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using ProceduralToolkit.Internal;
using Moq;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.EditorTests.UnitTests
{
    public class EditorDISettingsTest
    {
        private EditorDISettings settigns;
        private Mock<IContainer> container;

        private const float INJECTION_IDLE_TIME = 0.0001f;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            settigns = new GameObject().AddComponent<EditorDISettings>();
            settigns.InjectionIdleTime = INJECTION_IDLE_TIME;
            container = new Mock<IContainer>();
            settigns.Containers = new List<IContainer>
            {
                container.Object
            };
            settigns.SendMessage("Awake");
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(settigns);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestCleanInjectExecutedOnAwake()
        {
            yield return SkipFrames();
            container.Verify(mock => mock.CleanInject(), Times.Exactly(1));
        }

        [UnityTest]
        public IEnumerator TestInjectExecutedContinuously()
        {
            yield return SkipSeconds(0.1f);
            container.Verify(mock => mock.Inject(), Times.AtLeast(2));
        }

        [UnityTest]
        public IEnumerator TestCleanInjectiExecutedOnValidate()
        {
            settigns.SendMessage("OnValidate");

            yield return SkipFrames();

            //Should be called exacly 2 times: once on awake and once on validate.
            container.Verify(mock => mock.CleanInject(), Times.Exactly(2));
        }
    }
}
