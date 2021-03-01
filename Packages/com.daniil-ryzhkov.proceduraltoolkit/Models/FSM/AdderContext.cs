namespace ProceduralToolkit.Models.FSM
{
    public class AdderContext : FSMContext
    {
        public AdderContext(int columns)
        {
            Heights = new float[columns];
        }

        public float[] Heights { get; private set; }
    }
}
