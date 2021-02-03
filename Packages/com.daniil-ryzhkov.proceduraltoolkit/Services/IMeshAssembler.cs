using System;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public interface IMeshAssembler
    {
        event Action<Mesh> Generated;
        void Assemble();
    }
}