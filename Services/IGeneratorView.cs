using UnityEngine;

namespace ProceduralToolkit.Services
{
    public interface IGeneratorView
    {
        Mesh NewMesh { get; set; }
        void Update();
    }
}
