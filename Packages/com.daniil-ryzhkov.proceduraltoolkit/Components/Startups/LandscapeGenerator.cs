﻿using UnityEditor;
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
    [RequireComponent(typeof(ProceduralToolkit.Components.Generators.Plane), typeof(MeshAssemblerComponent))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        private IServiceContainer services;

        [SerializeReference]
        [HideInInspector]
        private GameObject view;

        private IGeneratorView MeshGeneratorView => view.GetComponent<IGeneratorView>();
        private IEnumerable<IGeneratorComponent> Generators => GetComponents<IGeneratorComponent>();

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
            foreach (var generator in Generators)
            {
                generator.Reset();
            }
        }

        private void RemoveOldHierarchy()
        {
            if (view != null)
            {
                UnityEngine.Object.DestroyImmediate(view);
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
            services.AddSingleton(Generator);
            services.AddSingleton<MeshBuilder>();
            services.AddSingleton<MeshAssembler>();
            services.GetService<IMeshAssembler>().Generated += (mesh) => MeshGeneratorView.NewMesh = mesh;
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
        }

        public void RegisterUndo()
        {
            Undo.RegisterCreatedObjectUndo(gameObject, "New Landscape Generator");
        }
    }
}