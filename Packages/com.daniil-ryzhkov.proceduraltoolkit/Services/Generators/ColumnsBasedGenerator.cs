namespace ProceduralToolkit.Services.Generators
{
    public abstract class ColumnsBasedGenerator : BaseVerticesGenerator
    {
        public int ColumnsInRow
        {
            get => columnsInRow;
            set => columnsInRow = value < 0 ? 0 : value;
        }
        private int columnsInRow;
    }
}
