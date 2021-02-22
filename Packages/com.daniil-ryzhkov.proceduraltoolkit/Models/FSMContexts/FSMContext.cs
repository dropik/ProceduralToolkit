using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Models.FSMContexts
{
    public sealed class FSMContext
    {
        public FSMContext(int columnsInRow)
        {
            ColumnsInRow = columnsInRow;
        }

        public IDiamondTilingState State { get; set; }
        public int ColumnsInRow { get; }
        public int Column { get; set; }
        public DiamondTilingContext DiamondTilingContext { get; set; }
    }
}
