using NUnit.Framework;
using ProceduralToolkit.UI;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.UITests
{
    public class ListFieldUxmlTest : BaseUxmlCustomVETest
    {
        protected override string TestLayout => "list-field-test";

        private VisualElement ElementsRoot => RootVisualElement.Query<Foldout>().First();
        private ListField TargetListField => RootVisualElement.Query<ListField>().First();

        [Test]
        public void TestSizeFieldCreated()
        {
            var sizeFieldsFound = ElementsRoot.Query<ListSizeField>().ToList();
            Assert.That(sizeFieldsFound.Count, Is.EqualTo(1));
            Assert.That(TargetListField.SizeField, Is.EqualTo(sizeFieldsFound[0]));
        }

        [Test]
        public void TestElementFactoryIsAssigned()
        {
            Assert.That(TargetListField.ElementFactory, Is.TypeOf<ListElementFactory>());
        }

        [Test]
        public void TestValueMapperAssigned()
        {
            Assert.That(TargetListField.ValueMapper, Is.TypeOf<ListFieldValueMapper>());
        }
    }
}
