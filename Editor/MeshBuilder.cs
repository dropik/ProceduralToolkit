using UnityEngine;
using System.Linq;

namespace ProceduralToolkit
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly IGenerator generator;
        private Mesh resultingMesh;

        public MeshBuilder(IGenerator generator)
        {
            this.generator = generator;
        }

        public Mesh Build()
        {
            resultingMesh = new Mesh();
            AddVertices();
            AddTriangles();
            resultingMesh.RecalculateNormals();
            return resultingMesh;
        }

        private void AddVertices()
        {
            var vertices = generator.Vertices.ToList();
            resultingMesh.vertices = vertices.ToArray();
        }

        private void AddTriangles()
        {
            var triangles = generator.Triangles.ToList();
            resultingMesh.triangles = triangles.ToArray();
        }
    }
}
