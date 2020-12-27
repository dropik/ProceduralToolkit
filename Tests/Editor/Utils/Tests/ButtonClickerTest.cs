using NUnit.Framework;
using ProceduralToolkit.EditorTests.Utils;
using System.Collections;
using UnityEditor;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static ProceduralToolkit.EditorTests.Utils.UIUtils;

namespace ProceduralToolkit.UtilsTests
{
    public class ButtonClickerTest
    {
        private EditorWindow window;
        private Button button;
        private ButtonClicker buttonClicker;

        [SetUp]
        public void SetUp()
        {
            window = EditorWindow.CreateWindow<EditorWindow>();
            button = new Button();
            window.rootVisualElement.Add(button);
            buttonClicker = new ButtonClicker();
        }

        [TearDown]
        public void TearDown()
        {
            window.Close();
        }

        [UnityTest]
        public IEnumerator TestButtonClick()
        {
            var wasClicked = false;
            button.clicked += () => { wasClicked = true; };
            buttonClicker.Click(button);

            yield return SkipFrames();

            Assert.That(wasClicked);
        }

        [UnityTest]
        public IEnumerator TestButtonClickWhenShouldNotBeClicked()
        {
            var wasClicked = false;
            buttonClicker.Click(button);

            yield return SkipFrames();

            Assert.IsFalse(wasClicked);
        }

        [UnityTest]
        public IEnumerator TestButtonClickOnNullButton()
        {
            var wasClicked = false;
            buttonClicker.Click(null);

            yield return SkipFrames();

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

            yield return SkipFrames();

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