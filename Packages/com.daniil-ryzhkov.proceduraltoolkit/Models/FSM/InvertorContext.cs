using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.Models.FSM
{
    public class InvertorContext : FSMContext
    {
        public InvertorContext(int columns)
        {
            Queue = new CircularBufferQueue<Vector3>(columns);
        }

        public CircularBufferQueue<Vector3> Queue { get; }
    }
}
