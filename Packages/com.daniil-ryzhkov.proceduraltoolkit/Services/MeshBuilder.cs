using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly Func<(IEnumerable<Vector3> vertices, IEnumerable<int> indices)> dataProvider;

        public MeshBuilder(Func<(IEnumerable<Vector3> vertices, IEnumerable<int> indices)> dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public Mesh Build()
        {
            var mesh = new Mesh();
            BuildToMesh(mesh);
            return mesh;
        }

        private void BuildToMesh(Mesh mesh)
        {
            var (vertices, indices) = dataProvider.Invoke();
            mesh.SetVertices(vertices.ToArray());
            mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
        }
    }
}
