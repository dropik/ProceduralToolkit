using ProceduralToolkit.Components.GeneratorSettings;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators.DiamondSquare;
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

        public void OnValidate()
        {
            ConfigureServices();
            InjectServices();
        }

        private void ConfigureServices()
        {
            services = ServiceContainerFactory.Create();

            services.AddSingleton(() => GetComponent<Terrain>().terrainData);
            services.AddSingleton(() => GetComponent<DiamondSquare>().settings);
            services.AddSingleton<GeneratorScheduler>();
            services.AddSingleton<LandscapeContext>();
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
        }

        private void InjectServices()
        {
            var scheduler = services.GetService<GeneratorScheduler>();

            var settings = GetComponent<IGeneratorSettings>();
            services.InjectServicesTo(settings);
            settings.Updated += scheduler.MarkDirty;

            EditorApplication.update += scheduler.Update;
        }

        public void OnDisable()
        {
            EditorApplication.update -= services.GetService<GeneratorScheduler>().Update;
        }
    }
}