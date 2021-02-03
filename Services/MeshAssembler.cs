using System;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class MeshAssembler : IMeshAssembler
    {
        private readonly IMeshBuilder meshBuilder;

        public MeshAssembler(IMeshBuilder meshBuilder)
        {
            this.meshBuilder = meshBuilder;
        }

        public event Action<Mesh> Generated;

        public void Assemble()
        {
            var generatedMesh = meshBuilder.Build();
            Generated?.Invoke(generatedMesh);
        }
    }
}
