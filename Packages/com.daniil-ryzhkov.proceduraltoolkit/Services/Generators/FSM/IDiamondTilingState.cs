using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IDiamondTilingState
    {
        Vector3? MoveNext(Vector3 vertex);
    }
}