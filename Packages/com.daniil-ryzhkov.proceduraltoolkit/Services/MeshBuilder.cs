using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly Func<IEnumerable<Vector3>> verticesProvider;
        private readonly LandscapeContext context;
        private readonly IIndicesGenerator indicesGenerator;

        public MeshBuilder(Func<IEnumerable<Vector3>> verticesProvider, LandscapeContext context, IIndicesGenerator indicesGenerator)
        {
            this.verticesProvider = verticesProvider;
            this.context = context;
            this.indicesGenerator = indicesGenerator;
        }

        public Mesh Build()
        {
            var mesh = new Mesh();
            BuildMesh(mesh);
            return mesh;
        }

        private void BuildMesh(Mesh mesh)
        {
            var vertices = verticesProvider();
            mesh.SetVertices(vertices.ToArray());
            indicesGenerator.Execute();
            mesh.SetIndices(context.Indices, MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
        }
    }
}
