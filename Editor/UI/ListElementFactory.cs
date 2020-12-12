using UnityEditor.UIElements;

namespace ProceduralToolkit.UI
{
    public class ListElementFactory : IListElementFactory
    {
        private IListField ListField { get; }

        public ListElementFactory(IListField listField)
        {
            ListField = listField;
        }

        public ObjectField CreateElement(int id)
        {
            return new ListElementField(ListField, id);
        }
    }
}
