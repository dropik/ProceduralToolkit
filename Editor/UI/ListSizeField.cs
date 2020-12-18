using UnityEditor.UIElements;

namespace ProceduralToolkit.UI
{
    public class ListSizeField : IntegerField
    {
        private readonly IListField listField;

        public ListSizeField(IListField listField)
        {
            this.listField = listField;
        }

        public override int value
        {
            get => base.value;
            set
            {
                if (value < 0)
                {
                    return;
                }
                HandleNewValue(value);
                base.value = value;
            }
        }

        private void HandleNewValue(int newValue)
        {
            if (newValue > value)
            {
                HandleIncrementedValue(newValue);
            }
            else if (newValue < value)
            {
                HandleDecrementedValue(newValue);
            }
        }

        private void HandleIncrementedValue(int newValue)
        {
            for (int i = value; i < newValue; i++)
            {
                listField.AddElement();
            }
        }

        private void HandleDecrementedValue(int newValue)
        {
            for (int i = value - 1; i >= newValue; i--)
            {
                listField.RemoveElement();
            }
        }
    }
}
