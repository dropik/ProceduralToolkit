using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [ExecuteInEditMode]
    public class TerrainGeneratorView : MonoBehaviour, IGeneratorView
    {
        [Service]
        private readonly Terrain terrain;

        public LandscapeContext NewContext { get; set; }

        public void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void Update()
        {
            if (NewContext != null)
            {
                var terrainData = terrain.terrainData;
                terrainData.SetHeights(0, 0, NewContext.Heights);
                NewContext = null;
            }
        }
    }
}
