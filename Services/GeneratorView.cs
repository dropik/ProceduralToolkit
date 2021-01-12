using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class GeneratorView : IGeneratorView
    {
        private readonly MeshFilter meshFilter;

        public GeneratorView(MeshFilter meshFilter)
        {
            this.meshFilter = meshFilter;
        }

        public Mesh NewMesh { get; set; }

        public void Update()
        {
            if (NewMesh != null)
            {
                meshFilter.sharedMesh = NewMesh;
                NewMesh = null;
            }
        }
    }
}
