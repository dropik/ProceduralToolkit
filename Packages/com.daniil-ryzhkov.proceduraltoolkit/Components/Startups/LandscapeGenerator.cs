using UnityEditor;
using UnityEngine;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Components.Generators;
using System.Collections.Generic;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Components.Startups
{
    [RequireComponent(typeof(DiamondSquare))]
    [RequireComponent(typeof(MeshAssemblerComponent))]
    public class LandscapeGenerator : Startup
    {
        private IServiceContainer services;

        [SerializeReference]
        [HideInInspector]
        private GameObject view;

        private IGeneratorView MeshGeneratorView => view.GetComponent<IGeneratorView>();
        private IEnumerable<IGeneratorSettings> GeneratorSettings => GetComponents<IGeneratorSettings>();

        public void Reset()
        {
            var resetter = new StartupResetter(gameObject)
            {
                DefaultName = "LandscapeGenerator",
                GeneratorSettings = GeneratorSettings
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
            SetupMeshAssemblerServices(services);
            SetupViewServices(services);
        }

        protected virtual void SetupMeshAssemblerServices(IServiceContainer services)
        {
            services.AddSingleton<LandscapeContext>();
            services.AddTransient(() => GetComponent<DiamondSquare>().Settings);
            services.AddTransient<IDisplacer, Displacer>();
            services.AddSingleton<IDsa>(() =>
            {
                var context = services.GetService<LandscapeContext>();
                var settings = services.GetService<DsaSettings>();
                context.Iterations = settings.Resolution;
                context.SideLength = settings.SideLength;
                return new Dsa(context,
                               new DiamondDsaStep(context, services.GetService<IDisplacer>()),
                               new SquareDsaStep(context, services.GetService<IDisplacer>()));
            });
            services.AddTransient<IIndicesGenerator, IndicesGenerator>();
            services.AddSingleton<MeshBuilder>();
            services.AddSingleton<MeshAssembler>();
        }

        protected virtual void SetupViewServices(IServiceContainer services)
        {
            services.AddSingleton(() => view.GetComponent<MeshFilter>());
        }

        private void InjectServices()
        {
            foreach (var generator in GeneratorSettings)
            {
                generator.Updated += services.GetService<IMeshAssembler>().Assemble;
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