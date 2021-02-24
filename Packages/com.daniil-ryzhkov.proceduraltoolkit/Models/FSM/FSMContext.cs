using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Models.FSM
{
    public sealed class FSMContext
    {
        public IState State { get; set; }
        public int Column { get; set; }
        public DiamondTilingContext DiamondTilingContext { get; set; }
        public RowDuplicatorContext RowDuplicatorContext { get; set; }
    }
}
