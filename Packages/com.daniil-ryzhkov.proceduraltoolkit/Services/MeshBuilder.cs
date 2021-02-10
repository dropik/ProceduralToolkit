using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly Func<IEnumerable<Vector3>> verticesProvider;
        private readonly Func<IEnumerable<int>> indicesProvider;

        public MeshBuilder(Func<IEnumerable<Vector3>> verticesProvider, Func<IEnumerable<int>> indicesProvider)
        {
            this.verticesProvider = verticesProvider;
            this.indicesProvider = indicesProvider;
        }

        public Mesh Build()
        {
            var mesh = new Mesh();
            BuildToMesh(mesh);
            return mesh;
        }

        private void BuildToMesh(Mesh mesh)
        {
            mesh.SetVertices(verticesProvider.Invoke().ToArray());
            mesh.SetIndices(indicesProvider.Invoke().ToArray(), MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
        }
    }
}
