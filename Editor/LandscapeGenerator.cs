using UnityEngine;

namespace ProceduralToolkit
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class LandscapeGenerator : MonoBehaviour
    {
        public float planeLength = 1f;

        private Mesh generatedMesh;

        public IGenerator Generator { get; set; }

        public void Generate()
        {
            GenerateMesh();
            UpdateMeshFilter();
            UpdateMaterial();
        }

        private void GenerateMesh()
        {
            var meshBuilder = new MeshBuilder(Generator);
            generatedMesh = meshBuilder.Build();
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

        private bool MaterialIsNotSet => GetComponent<MeshRenderer>().sharedMaterial == null;

        private void SetDefaultMaterial()
        {
            GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        }
    }
}
