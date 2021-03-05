﻿using ProceduralToolkit.Models;
using System;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    public class DiamondSquare : MonoBehaviour, IGeneratorSettings
    {
        [Min(0)]
        public int seed;
        [Range(0, 6)]
        public int iterations;
        public float magnitude;
        [Range(0, 1)]
        public float hardness;

        public DSASettings Settings =>
            new DSASettings { Seed = seed, Iterations = iterations, Magnitude = magnitude, Hardness = hardness };

        public event Action Updated;

        public void Reset()
        {
            seed = 0;
            iterations = 2;
            magnitude = 1;
            hardness = 0.5f;
        }

        public void OnValidate()
        {
            Updated?.Invoke();
        }
    }
}
