using UnityEditor;
using UnityEngine;
using ProceduralToolkit.Components.GeneratorSettings;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.Components.Startups
{
    [RequireComponent(typeof(PlaneSettings))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        private MeshAssembler meshAssembler;
        private IServiceContainer services;

        [SerializeReference]
        private GameObject view;

        private IGeneratorView MeshGeneratorView => view.GetComponent<IGeneratorView>();
        private PlaneSettings PlaneSettings => GetComponent<PlaneSettings>();

        public void Reset()
        {
            ResetName();
            ResetSettings();
            RemoveOldHierarchy();
            InitView();
        }

        private void ResetName()
        {
            name = "LandscapeGenerator";
        }

        private void ResetSettings()
        {
            PlaneSettings.Reset();
        }

        private void RemoveOldHierarchy()
        {
            if (view != null)
            {
                Object.DestroyImmediate(view);
            }
        }

        private void InitView()
        {
            view = new GameObject() { name = "view" };
            view.transform.parent = transform;
            view.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            view.AddComponent<MeshGeneratorView>();
        }

        public void OnValidate()
        {
            var generator = ConstructGenerator();
            InitMeshAssembler(generator);
            SetupUpdateCallbacks();
            ConfigureServices();
            InjectServices();
        }

        private IGenerator ConstructGenerator()
        {
            return PlaneSettings;
        }

        private void InitMeshAssembler(IGenerator generator)
        {
            meshAssembler = new MeshAssembler(new MeshBuilder(generator));
            meshAssembler.Generated += (mesh) => MeshGeneratorView.NewMesh = mesh;
        }

        private void SetupUpdateCallbacks()
        {
            PlaneSettings.GeneratorUpdated += meshAssembler.Assemble;
        }

        private void ConfigureServices()
        {
            services = ServiceContainerFactory.Create();
            services.AddSingleton(() => view.GetComponent<MeshFilter>());
        }

        private void InjectServices()
        {
            services.InjectServicesTo(MeshGeneratorView);
        }

        public void Start()
        {
            meshAssembler.Assemble();
        }

        public void RegisterUndo()
        {
            Undo.RegisterCreatedObjectUndo(gameObject, "New Landscape Generator");
        }
    }
}