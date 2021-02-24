using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Models.FSM
{
    public sealed class FSMContext
    {
        public FSMContext(int columnsInRow)
        {
            ColumnsInRow = columnsInRow;
        }

        public IStateBehaviour StateBehaviour { get; set; }
        public IState State { get; set; }
        public int ColumnsInRow { get; }
        public int Column { get; set; }
        public DiamondTilingContext DiamondTilingContext { get; set; }
        public RowDuplicatorContext RowDuplicatorContext { get; set; }
    }
}
