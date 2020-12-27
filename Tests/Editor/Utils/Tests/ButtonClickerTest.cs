using NUnit.Framework;
using ProceduralToolkit.EditorTests.Utils;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UtilsTests
{
    public class ButtonClickerTest
    {
        private EditorWindow window;
        private Button button;
        private ButtonClicker buttonClicker;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            window = EditorWindow.CreateWindow<EditorWindow>();
            button = new Button();
            window.rootVisualElement.Add(button);
            buttonClicker = new ButtonClicker();
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            window.Close();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestButtonClick()
        {
            var wasClicked = false;
            button.clicked += () => { wasClicked = true; };
            buttonClicker.Click(button);

            yield return SkipFrames(10);

            Assert.That(wasClicked);
        }

        private IEnumerator SkipFrames(int frames)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator TestButtonClickWhenShouldNotBeClicked()
        {
            var wasClicked = false;
            buttonClicker.Click(button);

            yield return SkipFrames(10);

            Assert.IsFalse(wasClicked);
        }

        [UnityTest]
        public IEnumerator TestButtonClickOnNullButton()
        {
            var wasClicked = false;
            buttonClicker.Click(null);

            yield return SkipFrames(10);

            Assert.IsFalse(wasClicked);
        }

        [UnityTest]
        public IEnumerator TestButtonClickWhenUpCallbackRegistered()
        {
            yield return TestWithCallback<PointerUpEvent>();
        }

        private IEnumerator TestWithCallback<TEvent>()
            where TEvent : EventBase<TEvent>, new()
        {
            var wasClicked = false;
            button.RegisterCallback<TEvent>((e) => { wasClicked = true; });
            buttonClicker.Click(button);

            yield return SkipFrames(10);

            Assert.That(wasClicked);
        }

        [UnityTest]
        public IEnumerator TestButtonClickWhenDownCallbackRegistered()
        {
            yield return TestWithCallback<PointerDownEvent>();
        }

        [UnityTest]
        public IEnumerator TestButtonClickWhenMouseUpRegistered()
        {
            yield return TestWithCallback<MouseUpEvent>();
        }
    }
}