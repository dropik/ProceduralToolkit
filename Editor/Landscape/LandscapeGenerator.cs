using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit.Landscape
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        private MeshAssembler meshAssembler;
        private Mesh updatedMesh;

        private PlaneSettings PlaneSettings => GetLazy<PlaneSettings>();

        private T GetLazy<T>()
            where T : Component
        {
            return GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        public void Awake()
        {
            if (name == "New Game Object")
            {
                name = "LandscapeGenerator";
            }
        }

        public void Reset()
        {
            PlaneSettings.Reset();
            SetDefaultMaterial();
        }

        private void SetDefaultMaterial()
        {
            GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        }

        public void OnValidate()
        {
            var generator = ConstructGenerator();
            InitMeshAssembler(generator);
            SetupUpdateCallbacks();
        }

        private IGenerator ConstructGenerator()
        {
            return PlaneSettings;
        }

        private void InitMeshAssembler(IGenerator generator)
        {
            meshAssembler = new MeshAssembler(new MeshBuilder(generator));
            meshAssembler.Generated += (mesh) => updatedMesh = mesh;
        }

        private void SetupUpdateCallbacks()
        {
            PlaneSettings.GeneratorUpdated += meshAssembler.Assemble;
        }

        public void Start()
        {
            meshAssembler.Assemble();
        }

        public void Update()
        {
            if (updatedMesh != null)
            {
                GetComponent<MeshFilter>().sharedMesh = updatedMesh;
                updatedMesh = null;
            }
        }
    }
}