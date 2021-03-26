using ProceduralToolkit.Components.GeneratorSettings;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators.DiamondSquare;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    [RequireComponent(typeof(DiamondSquare))]
    [RequireComponent(typeof(Terrain))]
    [RequireComponent(typeof(TerrainCollider))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        private IServiceContainer services;

        private IEnumerable<IGeneratorSettings> GeneratorSettings => GetComponents<IGeneratorSettings>();

        public void OnValidate()
        {
            ConfigureServices();
            InjectServices();
        }

        private void ConfigureServices()
        {
            services = ServiceContainerFactory.Create();
            SetupGeneratorStarterServices(services);
            SetupViewServices(services);
        }

        protected virtual void SetupGeneratorStarterServices(IServiceContainer services)
        {
            services.AddSingleton<LandscapeContext>();
            services.AddSingleton(() => GetComponent<DiamondSquare>().settings);
            services.AddTransient<IDisplacer, Displacer>();

            services.AddSingleton<IDsa>(() =>
            {
                var context = services.GetService<LandscapeContext>();
                var settings = services.GetService<DsaSettings>();
                var displacer = services.GetService<IDisplacer>();
                var terrainData = services.GetService<TerrainData>();
                
                return
                    new HeightsSaver(
                        new PredictableRandomizer(
                            new TerrainDataToContextConverter(
                                new HeightsInitializer(
                                    new Dsa(
                                        context,
                                        new DiamondStep(context, displacer),
                                        new SquareStep(context, displacer)
                                    ),
                                    context,
                                    settings
                                ),
                                terrainData,
                                context
                            ),
                            settings
                        ),
                        terrainData,
                        context
                    );
            });

            services.AddSingleton<GeneratorScheduler>();
        }

        protected virtual void SetupViewServices(IServiceContainer services)
        {
            services.AddSingleton(() => GetComponent<Terrain>().terrainData);
        }

        private void InjectServices()
        {
            var scheduler = services.GetService<GeneratorScheduler>();
            foreach (var generator in GeneratorSettings)
            {
                services.InjectServicesTo(generator);
                generator.Updated += scheduler.MarkDirty;
            }

            EditorApplication.update += scheduler.Update;
        }

        public void OnDisable()
        {
            EditorApplication.update -= services.GetService<GeneratorScheduler>().Update;
        }
    }
}