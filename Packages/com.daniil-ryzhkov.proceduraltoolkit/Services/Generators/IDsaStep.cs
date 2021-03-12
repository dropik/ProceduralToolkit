using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public interface IDsaStep
    {
        void Execute(Vector3[] vertices, int iteration);
    }
}
