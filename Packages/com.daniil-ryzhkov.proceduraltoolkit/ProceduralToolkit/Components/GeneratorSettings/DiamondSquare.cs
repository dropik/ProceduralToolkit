using ProceduralToolkit.Models;
using System;
using UnityEngine;

namespace ProceduralToolkit.Components.GeneratorSettings
{
    [ExecuteInEditMode]
    public class DiamondSquare : MonoBehaviour, IGeneratorSettings
    {
        [SerializeField]
        public DsaSettings settings;

        public event Action Updated;

        public void Reset()
        {
            settings = new DsaSettings();
        }

        public void OnValidate()
        {
            Updated?.Invoke();
        }

        public void OnTerrainChanged(TerrainChangedFlags flags)
        {
            if ((flags & TerrainChangedFlags.HeightmapResolution) != 0)
            {
                Updated?.Invoke();
            }
        }
    }
}