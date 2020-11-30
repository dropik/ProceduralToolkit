using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public partial class SliderWithField : Slider
    {
        private FloatField field;

        public SliderWithField()
        {
            InitField();
            Add(field);
        }

        private void InitField()
        {
            field = new FloatField();
            field.style.width = 50;
            field.RegisterValueChangedCallback(UpdateGlobalValue);
        }

        private void UpdateGlobalValue(ChangeEvent<float> e)
        {
            // Without notify because we don't want a cycle of value updates
            SetValueWithoutNotify(e.newValue);
        }

        public override float value
        {
            get => base.value;
            set
            {
                base.value = value;

                // Without notify because we don't want a cycle of value updates
                field?.SetValueWithoutNotify(value);
            }
        }
    }
}
