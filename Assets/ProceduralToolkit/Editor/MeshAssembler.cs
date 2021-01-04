﻿using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    public class MeshAssembler
    {
        private readonly IMeshBuilder meshBuilder;

        public MeshAssembler(IMeshBuilder meshBuilder)
        {
            this.meshBuilder = meshBuilder;
        }

        public event System.Action<Mesh> Generated;

        public void Assemble()
        {
            var generatedMesh = meshBuilder.Build();
            Generated?.Invoke(generatedMesh);
        }
    }
}