using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public partial class ListField : BaseField<IList<Object>>, IListField
    {
        public new class UxmlFactory : UxmlFactory<ListField, UxmlTraits>
        {
            public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
            {
                var listField = base.Create(bag, cc) as ListField;
                var sizeField = new ListSizeField(listField);
                listField.SizeField = sizeField;
                listField.ElementFactory = new ListElementFactory(listField);
                listField.ValueMapper = new ListFieldValueMapper(listField);

                return listField;
            }
        }

        public new class UxmlTraits : BaseField<IList<Object>>.UxmlTraits { }
    }
}
