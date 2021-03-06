﻿using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators.DiamondSquare;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Components.Startups
{
    [RequireComponent(typeof(DiamondSquare))]
    [RequireComponent(typeof(GeneratorStarterComponent))]
    [RequireComponent(typeof(Terrain))]
    [RequireComponent(typeof(TerrainCollider))]
    [RequireComponent(typeof(TerrainView))]
    public class LandscapeGenerator : MonoBehaviour
    {
        private IServiceContainer services;

        private IView View => GetComponent<IView>();
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
            services.AddSingleton<DsaSettings>();
            services.AddTransient<IDisplacer, Displacer>();

            services.AddSingleton<IDsa>(() =>
            {
                var context = services.GetService<LandscapeContext>();
                var settings = services.GetService<DsaSettings>();
                var displacer = services.GetService<IDisplacer>();
                
                return
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
                            services.GetService<TerrainData>(),
                            context
                        ),
                        settings
                    );
            });

            services.AddSingleton<GeneratorStarter>();
        }

        protected virtual void SetupViewServices(IServiceContainer services)
        {
            services.AddSingleton(() => GetComponent<Terrain>().terrainData);
        }

        private void InjectServices()
        {
            foreach (var generator in GeneratorSettings)
            {
                services.InjectServicesTo(generator);
                generator.Updated += services.GetService<IGeneratorStarter>().Start;
            }
            services.InjectServicesTo(GetComponent<GeneratorStarterComponent>());
            services.InjectServicesTo(View);
            services.GetService<IGeneratorStarter>().Generated += View.MarkDirty;
        }
    }
}