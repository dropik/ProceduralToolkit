using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public partial class SliderWithField
    {
        public new class UxmlFactory : UxmlFactory<SliderWithField, UxmlTraits> { }

        public new class UxmlTraits : Slider.UxmlTraits
        {
            private SliderWithField slider;

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                slider = ve as SliderWithField;
                ParseAttributes(bag);
            }

            private void ParseAttributes(IUxmlAttributes bag)
            {
                ParseValueAttribute(bag);
            }

            private void ParseValueAttribute(IUxmlAttributes bag)
            {
                if (bag.TryGetAttributeValue("value", out string valueString))
                {
                    slider.value = float.Parse(valueString);
                }
            }
        }
    }
}
