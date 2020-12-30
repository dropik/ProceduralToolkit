using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    public class LandscapeGenerator : MonoBehaviour
    {
        private Mesh generatedMesh;

        public IMeshBuilder MeshBuilder { get; set; }
        public IMaterialProvider DefaultMaterialProvider { get; set; }
        public IMeshContainer MeshContainer { get; set; }
        public IMaterialContainer MaterialContainer { get; set; }

        public void Reset()
        {
        }

        public void Generate()
        {
            GenerateMesh();
            UpdateMeshContainer();
            UpdateMaterial();
        }

        private void GenerateMesh()
        {
            generatedMesh = MeshBuilder.Build();
        }

        private void UpdateMeshContainer()
        {
            MeshContainer.Mesh = generatedMesh;
        }

        private void UpdateMaterial()
        {
            if (MaterialIsNotSet)
            {
                SetDefaultMaterial();
            }
        }

        private bool MaterialIsNotSet => MaterialContainer.Material == null;

        private void SetDefaultMaterial()
        {
            MaterialContainer.Material = DefaultMaterialProvider.GetMaterial();
        }
    }
}
