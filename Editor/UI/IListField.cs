namespace ProceduralToolkit.UI
{
    public interface IListField
    {
        System.Type ObjectType { get; set; }
        void AddElement();
        void RemoveElement();
        void UpdateValueAt(int id);
    }
}
