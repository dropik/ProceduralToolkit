using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class MenuEntries
    {
        [MenuItem("GameObject/ProceduralToolkit/Landscape Generator")]
        public static void NewLandscapeGenerator()
        {
            var obj = new GameObject
            {
                name = "Landscape Generator"
            };
            Undo.RegisterCreatedObjectUndo(obj, "New Landscape Generator");

            var terrainData = new TerrainData
            {
                heightmapResolution = 129,
                size = new Vector3(1000, 1000, 1000)
            };

            var terrain = obj.AddComponent<Terrain>();
            terrain.terrainData = terrainData;
            terrain.materialTemplate = new Material(Shader.Find("Nature/Terrain/Standard"));

            obj.AddComponent<TerrainCollider>().terrainData = terrainData;

            var generator = obj.AddComponent<LandscapeGenerator>();
        }
    }
}
