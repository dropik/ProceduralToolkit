using UnityEditor;
using UnityEngine;
using ProceduralToolkit.Components.GeneratorSettings;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.Components.Startups
{
    [RequireComponent(typeof(PlaneSettings), typeof(MeshAssemblerComponent))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
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
            ConfigureServices();
            InjectServices();
            SetupUpdateCallbacks();
        }

        private void ConfigureServices()
        {
            services = ServiceContainerFactory.Create();
            services.AddSingleton<IGenerator>(Generator);
            services.AddSingleton<MeshBuilder>();
            services.AddSingleton<MeshAssembler>();
            services.GetService<IMeshAssembler>().Generated += (mesh) => MeshGeneratorView.NewMesh = mesh;
            services.AddSingleton(() => view.GetComponent<MeshFilter>());
        }

        private void InjectServices()
        {
            services.InjectServicesTo(GetComponent<MeshAssemblerComponent>());
            services.InjectServicesTo(MeshGeneratorView);
        }
        private void SetupUpdateCallbacks()
        {
            PlaneSettings.GeneratorUpdated += services.GetService<IMeshAssembler>().Assemble;
        }

        private IGenerator Generator => PlaneSettings;

        public void RegisterUndo()
        {
            Undo.RegisterCreatedObjectUndo(gameObject, "New Landscape Generator");
        }
    }
}