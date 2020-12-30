using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    public class MeshAssembler
    {
        private readonly IMeshBuilder meshBuilder;
        private readonly IMaterialProvider defaultMaterialProvider;
        private readonly IMeshContainer meshContainer;
        private readonly IMaterialContainer materialContainer;

        private Mesh generatedMesh;

        public MeshAssembler(IMeshBuilder meshBuilder,
                                  IMaterialProvider defaultMaterialProvider,
                                  IMeshContainer meshContainer,
                                  IMaterialContainer materialContainer)
        {
            this.meshBuilder = meshBuilder;
            this.defaultMaterialProvider = defaultMaterialProvider;
            this.meshContainer = meshContainer;
            this.materialContainer = materialContainer;
        }

        public void Assemble()
        {
            BuildMesh();
            UpdateMeshContainer();
            UpdateMaterialContainer();
        }

        private void BuildMesh()
        {
            generatedMesh = meshBuilder.Build();
        }

        private void UpdateMeshContainer()
        {
            meshContainer.Mesh = generatedMesh;
        }

        private void UpdateMaterialContainer()
        {
            if (MaterialIsNotSet)
            {
                SetDefaultMaterial();
            }
        }

        private bool MaterialIsNotSet => materialContainer.Material == null;

        private void SetDefaultMaterial()
        {
            materialContainer.Material = defaultMaterialProvider.GetMaterial();
        }
    }
}
