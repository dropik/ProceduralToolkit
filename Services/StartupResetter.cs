using System;
using System.Collections.Generic;
using ProceduralToolkit.Components.Generators;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class StartupResetter
    {
        private readonly GameObject gameObject;
        private readonly IList<GameObject> children;

        public StartupResetter(GameObject gameObject)
        {
            this.gameObject = gameObject;
            children = new List<GameObject>();
            Generators = new IGeneratorComponent[0];
        }

        public string DefaultName { get; set; }
        public IEnumerable<IGeneratorComponent> Generators { get; set; }
        public event Func<GameObject> InitChild;

        public void Reset()
        {
            ResetName();
            ResetGenerators();
            RebuildHierarchy();
        }

        private void ResetName()
        {
            gameObject.name = DefaultName;
        }

        private void ResetGenerators()
        {
            foreach (var generator in Generators)
            {
                generator.Reset();
            }
        }

        private void RebuildHierarchy()
        {
            DestroyChildren();
            InitChildren();
        }

        private void DestroyChildren()
        {
            foreach (var child in children)
            {
                UnityEngine.Object.DestroyImmediate(child);
            }
            children.Clear();
        }

        private void InitChildren()
        {
            if (InitChild != null)
            {
                foreach (Func<GameObject> init in InitChild.GetInvocationList())
                {
                    children.Add(init());
                }
            }
        }
    }
}