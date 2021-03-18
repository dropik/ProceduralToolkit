using ProceduralToolkit.Services.Generators;
using System;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class MeshAssembler : IMeshAssembler
    {
        private readonly IMeshBuilder meshBuilder;
        private readonly IDsa dsa;

        public MeshAssembler(IMeshBuilder meshBuilder, IDsa dsa)
        {
            this.meshBuilder = meshBuilder;
            this.dsa = dsa;
        }

        public event Action<Mesh> Generated;

        public void Assemble()
        {
            dsa.Execute();
            var generatedMesh = meshBuilder.Build();
            Generated?.Invoke(generatedMesh);
        }
    }
}
