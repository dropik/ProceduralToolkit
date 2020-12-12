using UnityEditor.UIElements;
using UnityEngine;

namespace ProceduralToolkit.UI
{
    public class ListElementField : ObjectField
    {
        private IListField ListField { get; }
        private int Id { get; }

        public ListElementField(IListField listField, int id) : base()
        {
            ListField = listField;
            Id = id;
            name = $"element{id}";
            label = $"Element {id}";
            objectType = listField.ObjectType;
        }

        public override Object value
        {
            get => base.value;
            set
            {
                base.value = value;
                ListField.UpdateValueAt(Id);
            }
        }
    }
}
