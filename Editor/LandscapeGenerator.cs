using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class LandscapeGenerator : MonoBehaviour
    {
        private Mesh generatedMesh;

        public IMeshBuilder MeshBuilder { get; set; }
        public IMaterialProvider DefaultMaterialProvider { get; set; }

        public void Generate()
        {
            GenerateMesh();
            UpdateMeshFilter();
            UpdateMaterial();
        }

        private void GenerateMesh()
        {
            generatedMesh = MeshBuilder.Build();
        }

        private void UpdateMeshFilter()
        {
            GetComponent<MeshFilter>().sharedMesh = generatedMesh;
        }

        private void UpdateMaterial()
        {
            if (MaterialIsNotSet)
            {
                SetDefaultMaterial();
            }
        }

        private bool MaterialIsNotSet => MeshRenderer.sharedMaterial == null;

        private MeshRenderer MeshRenderer => GetComponent<MeshRenderer>();

        private void SetDefaultMaterial()
        {
            MeshRenderer.sharedMaterial = DefaultMaterialProvider.GetMaterial();
        }
    }
}
