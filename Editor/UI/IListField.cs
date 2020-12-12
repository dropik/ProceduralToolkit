namespace ProceduralToolkit.UI
{
    public interface IListField
    {
        System.Type ObjectType { get; set; }
        void UpdateValueAt(int id);
    }
}
