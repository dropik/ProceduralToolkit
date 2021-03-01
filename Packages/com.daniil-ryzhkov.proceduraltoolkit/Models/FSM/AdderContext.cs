using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Models.FSM
{
    public class AdderContext : FSMContext
    {
        public AdderContext(int columns)
        {
            Heights = new CircularBufferQueue<float>(columns);
        }

        public CircularBufferQueue<float> Heights { get; private set; }
    }
}
