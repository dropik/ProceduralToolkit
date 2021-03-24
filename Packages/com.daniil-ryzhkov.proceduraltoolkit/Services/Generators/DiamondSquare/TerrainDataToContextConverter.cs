using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class TerrainDataToContextConverter : BaseDsaDecorator
    {
        private readonly TerrainData terrainData;
        private readonly LandscapeContext context;

        public TerrainDataToContextConverter(IDsa wrappee, TerrainData terrainData, LandscapeContext context)
            : base(wrappee)
        {
            this.terrainData = terrainData;
            this.context = context;
        }

        public override void Execute()
        {
            ConvertData();
            base.Execute();
        }

        private void ConvertData()
        {
            var resolution = terrainData.heightmapResolution;
            context.Length = resolution;
            context.Iterations = (int)Mathf.Log(resolution - 1, 2);
        }
    }
}