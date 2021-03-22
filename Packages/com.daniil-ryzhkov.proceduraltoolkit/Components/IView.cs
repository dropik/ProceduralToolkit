using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    public interface IView
    {
        void MarkDirty();
        bool IsDirty { get; }
        void Update();
    }
}
