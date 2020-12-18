using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace ProceduralToolkit.UI
{
    public interface IListField
    {
        System.Type ObjectType { get; set; }
        IntegerField SizeField { get; set; }
        VisualElement ElementsRoot { get; }
        void AddElement();
        void RemoveElement();
    }
}
