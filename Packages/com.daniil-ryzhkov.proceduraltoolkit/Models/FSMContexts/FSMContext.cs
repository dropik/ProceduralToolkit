using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Models.FSMContexts
{
    public class FSMContext
    {
        public FSMContext(int columnsInRow)
        {
            ColumnsInRow = columnsInRow;
        }

        public IDiamondTilingState State { get; set; }
        public int ColumnsInRow { get; }
        public int Column { get; set; }
    }
}
