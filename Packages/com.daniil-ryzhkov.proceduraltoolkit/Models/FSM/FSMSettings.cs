namespace ProceduralToolkit.Models.FSM
{
    public sealed class FSMSettings
    {
        public FSMContext FSMContext { get; set; }
        public int ColumnsLimit { get; set; }
        public bool ZeroColumnOnLimitReached { get; set; } = true;
    }
}
