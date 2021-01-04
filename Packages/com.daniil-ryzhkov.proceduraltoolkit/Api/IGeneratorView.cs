using UnityEngine;

namespace ProceduralToolkit.Api
{
    public interface IGeneratorView
    {
        Mesh NewMesh { get; set; }
        void Update();
    }
}
