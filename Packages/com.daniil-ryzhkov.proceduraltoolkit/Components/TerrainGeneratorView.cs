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
        [Service]
        private readonly DsaSettings settings;

        public Mesh NewMesh { get; set; }

        public void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void Update()
        {
            if (NewMesh != null)
            {
                var terrainData = terrain.terrainData;
                var resolution = terrainData.heightmapResolution;
                var index = 0;
                var heights = new float[resolution, resolution];
                var vertices = NewMesh.vertices;
                var range = settings.Magnitude * 2;
                for (int row = 0; row < resolution; row++)
                {
                    for (int column = 0; column < resolution; column++)
                    {
                        heights[row, column] = vertices[index].y / range + 0.5f;
                        index++;
                    }
                }

                terrainData.SetHeights(0, 0, heights);
                NewMesh = null;
            }
        }
    }
}
