using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    public interface IGeneratorView
    {
        LandscapeContext NewContext { get; set; }
        void Update();
    }
}
