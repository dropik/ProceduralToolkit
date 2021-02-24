using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IVertexPreprocessor
    {
        void Process(Vector3 vertex);
    }
}
