using UnityEditor;
using UnityEngine;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Components.Generators;
using System.Collections.Generic;
using ProceduralToolkit.Models;
using System;
using System.Linq;

namespace ProceduralToolkit.Components.Startups
{
    [RequireComponent(typeof(Generators.Plane))]
    [RequireComponent(typeof(MeshAssemblerComponent))]
    public class LandscapeGenerator : Startup
    {
        private IServiceContainer services;

        [SerializeReference]
        [HideInInspector]
        private GameObject view;

        private IGeneratorView MeshGeneratorView => view.GetComponent<IGeneratorView>();
        private IEnumerable<IGeneratorComponent> Generators => GetComponents<IGeneratorComponent>();

        public void Reset()
        {
            var resetter = new StartupResetter(gameObject)
            {
                DefaultName = "LandscapeGenerator",
                Generators = Generators
            };
            resetter.InitChild += InitView;
            resetter.Reset();
        }
        
        private GameObject InitView()
        {
            view = new GameObject() { name = "view" };
            view.transform.parent = transform;
            view.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            view.AddComponent<MeshGeneratorView>();
            return view;
        }

        public void OnValidate()
        {
            ConfigureServices();
            InjectServices();
        }

        private void ConfigureServices()
        {
            services = ServiceContainerFactory.Create();
            SetupGeneratorServices(services);
            SetupMeshAssemblerServices(services);
            SetupViewServices(services);
        }

        protected virtual void SetupGeneratorServices(IServiceContainer services)
        {
            services.AddSingleton<Func<PlaneGeneratorSettings, IGenerator>>(settings => new PlaneGenerator(settings));
        }

        protected virtual void SetupMeshAssemblerServices(IServiceContainer services)
        {
            services.AddSingleton<Func<(IEnumerable<Vector3> vertices, IEnumerable<int> indices)>>(() =>
            {
                return (Generator.Vertices, Generator.Triangles);
            });
            services.AddSingleton<MeshBuilder>();
            services.AddSingleton<MeshAssembler>();
        }

        protected virtual void SetupViewServices(IServiceContainer services)
        {
            services.AddSingleton(() => view.GetComponent<MeshFilter>());
        }

        private IGenerator Generator => Generators.First();

        private void InjectServices()
        {
            foreach (var generator in Generators)
            {
                services.InjectServicesTo(generator);
                generator.GeneratorUpdated += services.GetService<IMeshAssembler>().Assemble;
            }
            services.InjectServicesTo(GetComponent<MeshAssemblerComponent>());
            services.InjectServicesTo(MeshGeneratorView);
            services.GetService<IMeshAssembler>().Generated += (mesh) => MeshGeneratorView.NewMesh = mesh;
        }

        public override void RegisterUndo()
        {
            Undo.RegisterCreatedObjectUndo(gameObject, "New Landscape Generator");
        }
    }
}