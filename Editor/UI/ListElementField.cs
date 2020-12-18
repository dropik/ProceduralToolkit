using UnityEditor.UIElements;

namespace ProceduralToolkit.UI
{
    public class ListElementField : ObjectField
    {
        public ListElementField(IListField listField, int id)
        {
            name = $"element{id}";
            label = $"Element {id}";
            objectType = listField.ObjectType;
        }
    }
}
