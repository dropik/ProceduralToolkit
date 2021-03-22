using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class DsaPregenerationSetup : IDsa
    {
        private readonly IDsa wrappee;
        private readonly DsaSettings settings;
        private readonly LandscapeContext context;

        public DsaPregenerationSetup(IDsa wrappee, DsaSettings settings, LandscapeContext context)
        {
            this.wrappee = wrappee;
            this.settings = settings;
            this.context = context;
        }

        public void Execute()
        {
            SetupInitialValues();
            wrappee.Execute();
        }

        private void SetupInitialValues()
        {
            CopySettingsToContext();
            CalculateLength();
            context.Heights = CreateHeightsBuffer();
        }

        private void CopySettingsToContext()
        {
            context.Iterations = settings.Resolution;
        }

        private void CalculateLength()
        {
            context.Length = (int)Mathf.Pow(2, context.Iterations) + 1;
        }

        private float[,] CreateHeightsBuffer()
        {
            var heights = new float[context.Length, context.Length];
            SetCornerHeights(heights);
            return heights;
        }

        private void SetCornerHeights(float[,] heights)
        {
            heights[0, 0] = 0.5f;
            heights[0, context.Length - 1] = 0.5f;
            heights[context.Length - 1, 0] = 0.5f;
            heights[context.Length - 1, context.Length - 1] = 0.5f;
        }
    }
}
