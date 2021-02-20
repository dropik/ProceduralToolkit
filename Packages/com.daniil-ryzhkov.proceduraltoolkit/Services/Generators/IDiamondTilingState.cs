using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public interface IDiamondTilingState
    {
        Vector3? MoveNext(Vector3 vertex);
    }
}