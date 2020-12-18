using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using Moq;
using ProceduralToolkit.UI;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListSizeFieldTest : BaseCustomVETest
    {
        private ListSizeField sizeField;
        private Mock<IListField> listField;

        protected override void PreWindowCreation()
        {
            listField = new Mock<IListField>();
        }

        protected override VisualElement CreateTestTarget()
        {
            sizeField = new ListSizeField(listField.Object);
            return sizeField;
        }

        [Test]
        public void TestInitialValue()
        {
            Assert.That(sizeField.value, Is.Zero);
        }

        [Test]
        public void TestIncrementedValueCallsAddElement ()
        {
            const int NEW_VALUE = 2;

            sizeField.value = NEW_VALUE;

            listField.Verify(mock => mock.AddElement(), Times.Exactly(NEW_VALUE));
        }

        [Test]
        public void TestDecrementedValueCallsRemoveElement()
        {
            const int FIRST_VALUE = 5;
            const int LAST_VALUE = 2;
            sizeField.value = FIRST_VALUE;
            sizeField.value = LAST_VALUE;
            listField.Verify(mock => mock.RemoveElement(), Times.Exactly(FIRST_VALUE - LAST_VALUE));
        }

        [Test]
        public void TestNewValueLessThanZeroDoesNotDoAnyChange()
        {
            const int FIRST_VALUE = 3;
            const int NEW_VALUE = -1;
            sizeField.value = FIRST_VALUE;
            sizeField.value = NEW_VALUE;
            Assert.That(sizeField.value, Is.EqualTo(FIRST_VALUE));
            listField.Verify(mock => mock.RemoveElement(), Times.Never);
        }
    }
}
