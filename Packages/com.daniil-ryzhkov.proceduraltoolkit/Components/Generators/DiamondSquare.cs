using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using System;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    [ExecuteInEditMode]
    public class DiamondSquare : MonoBehaviour, IGeneratorSettings
    {
        [Service]
        private readonly DsaSettings settings;

        [Min(0)]
        public int seed;

        [Range(0, 1)]
        public float magnitude;

        [Range(0, 2)]
        public float hardness;

        public event Action Updated;

        public void Reset()
        {
            seed = 0;
            magnitude = 1;
            hardness = 1;
        }

        public void OnValidate()
        {
            TryUpdateSettings();
            Updated?.Invoke();
        }

        private void TryUpdateSettings()
        {
            if (settings != null)
            {
                settings.Seed = seed;
                settings.Magnitude = magnitude;
                settings.Hardness = hardness;
            }
        }

        public void OnTerrainChanged(TerrainChangedFlags flags)
        {
            if ((flags & TerrainChangedFlags.HeightmapResolution) != 0)
            {
                TryUpdateSettings();
                Updated?.Invoke();
            }
        }
    }
}