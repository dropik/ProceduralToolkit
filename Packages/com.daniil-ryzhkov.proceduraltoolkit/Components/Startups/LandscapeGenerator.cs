using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators;
using ProceduralToolkit.Services.Generators.DiamondSquare;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit.Components.Startups
{
    [RequireComponent(typeof(DiamondSquare))]
    [RequireComponent(typeof(GeneratorStarterComponent))]
    [RequireComponent(typeof(Terrain))]
    [RequireComponent(typeof(TerrainCollider))]
    [RequireComponent(typeof(TerrainGeneratorView))]
    public class LandscapeGenerator : Startup
    {
        private IServiceContainer services;

        private IGeneratorView View => GetComponent<IGeneratorView>();
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
            var terrainData = new TerrainData
            {
                heightmapResolution = 33,
                size = new Vector3(1000, 1000, 1000)
            };

            var terrain = GetComponent<Terrain>();
            terrain.terrainData = terrainData;
            terrain.materialTemplate = new Material(Shader.Find("Nature/Terrain/Standard"));

            GetComponent<TerrainCollider>().terrainData = terrainData;

            return default;
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
            services.AddSingleton<DsaSettings>();
            services.AddTransient<IDisplacer, Displacer>();
            services.AddSingleton<IDsa>(() =>
            {
                var context = services.GetService<LandscapeContext>();
                var settings = services.GetService<DsaSettings>();
                return new DsaPregenerationSetup(new Dsa(context,
                                                         new DiamondDsaStep(context, services.GetService<IDisplacer>()),
                                                         new SquareDsaStep(context, services.GetService<IDisplacer>())),
                                                 settings,
                                                 context);
            });
            services.AddTransient<IIndicesGenerator, IndicesGenerator>();
            services.AddSingleton<MeshBuilder>();
            services.AddSingleton<MeshAssembler>();
        }

        protected virtual void SetupViewServices(IServiceContainer services)
        {
            services.AddSingleton(() => GetComponent<Terrain>());
        }

        private void InjectServices()
        {
            foreach (var generator in GeneratorSettings)
            {
                services.InjectServicesTo(generator);
                generator.Updated += services.GetService<IMeshAssembler>().Assemble;
            }
            services.InjectServicesTo(GetComponent<GeneratorStarterComponent>());
            services.InjectServicesTo(View);
            services.GetService<IMeshAssembler>().Generated += (mesh) => View.NewContext = services.GetService<LandscapeContext>();
        }

        public override void RegisterUndo()
        {
            Undo.RegisterCreatedObjectUndo(gameObject, "New Landscape Generator");
        }
    }
}