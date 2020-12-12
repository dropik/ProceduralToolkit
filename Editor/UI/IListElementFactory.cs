using UnityEditor.UIElements;

namespace ProceduralToolkit.UI
{
    public interface IListElementFactory
    {
        ObjectField CreateElement(int id);
    }
}
