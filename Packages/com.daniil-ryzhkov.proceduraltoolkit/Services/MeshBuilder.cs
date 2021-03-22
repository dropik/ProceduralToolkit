using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly LandscapeContext context;
        private readonly IIndicesGenerator indicesGenerator;

        public MeshBuilder(LandscapeContext context, IIndicesGenerator indicesGenerator)
        {
            this.context = context;
            this.indicesGenerator = indicesGenerator;
        }

        public Mesh Build()
        {
            var mesh = new Mesh();
            BuildMesh(mesh);
            context.Mesh = mesh;
            return mesh;
        }

        private void BuildMesh(Mesh mesh)
        {
            mesh.SetVertices(context.Vertices);
            indicesGenerator.Execute();
            mesh.SetIndices(context.Indices, MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
        }
    }
}
