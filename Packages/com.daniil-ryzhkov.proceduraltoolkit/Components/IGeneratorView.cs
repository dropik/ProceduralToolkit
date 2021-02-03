using UnityEngine;

namespace ProceduralToolkit.Components
{
    public interface IGeneratorView
    {
        Mesh NewMesh { get; set; }
        void Update();
    }
}
