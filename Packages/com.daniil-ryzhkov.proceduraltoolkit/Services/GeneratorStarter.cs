using ProceduralToolkit.Services.Generators.DiamondSquare;
using System;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    public class GeneratorStarter : IGeneratorStarter
    {
        private readonly IDsa dsa;

        public GeneratorStarter(IDsa dsa)
        {
            this.dsa = dsa;
        }

        public event Action Generated;

        public void Start()
        {
            dsa.Execute();
            Generated?.Invoke();
        }
    }
}
