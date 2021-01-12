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
        }

        public override float value
        {
            get => field.value;
            set
            {
                if (field != null)
                {
                    field.value = value;
                }
            }
        }
    }
}
