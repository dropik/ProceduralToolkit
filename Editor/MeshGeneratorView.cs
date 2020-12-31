using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit
{
    public class MeshGeneratorView : IGeneratorView
    {
        private readonly IMeshContainer meshContainer;

        public MeshGeneratorView(IMeshContainer meshContainer)
        {
            this.meshContainer = meshContainer;
        }

        public void OnGenerate(Mesh mesh)
        {
            meshContainer.Mesh = mesh;
        }
    }
}
