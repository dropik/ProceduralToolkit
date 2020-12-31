using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    public class MeshAssembler
    {
        private readonly IMeshBuilder meshBuilder;
        private readonly IGeneratorView meshGeneratorView;
        private readonly IGeneratorView materialGeneratorView;

        private Mesh generatedMesh;

        public MeshAssembler(IMeshBuilder meshBuilder,
                             IGeneratorView meshGeneratorView,
                             IGeneratorView materialGeneratorView)
        {
            this.meshBuilder = meshBuilder;
            this.meshGeneratorView = meshGeneratorView;
            this.materialGeneratorView = materialGeneratorView;
        }

        public void Assemble()
        {
            BuildMesh();
            UpdateMesh();
            UpdateMaterial();
        }

        private void BuildMesh()
        {
            generatedMesh = meshBuilder.Build();
        }

        private void UpdateMesh()
        {
            meshGeneratorView.OnGenerate(generatedMesh);
        }

        private void UpdateMaterial()
        {
            materialGeneratorView.OnGenerate(generatedMesh);
        }
    }
}
