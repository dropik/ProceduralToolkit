using System;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    public class DiamondSquare : MonoBehaviour, IGeneratorSettings
    {
        [Min(0)]
        public int seed;

        [Range(0, 7)]
        public int resolution;

        [Min(0)]
        public float sideLength;

        [Min(0)]
        public float magnitude;

        [Range(0, 2)]
        public float hardness;

        public event Action Updated;

        public DsaSettings Settings => new DsaSettings
        {
            Seed = seed,
            Resolution = resolution,
            SideLength = sideLength,
            Magnitude = magnitude,
            Hardness = hardness
        };

        public void Reset()
        {
            seed = 0;
            resolution = 5;
            sideLength = 1000;
            magnitude = 100;
            hardness = 0.5f;
        }

        public void OnValidate()
        {
            Updated?.Invoke();
        }
    }
}