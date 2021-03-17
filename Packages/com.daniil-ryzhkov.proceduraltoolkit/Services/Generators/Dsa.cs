using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Dsa
    {
        private readonly LandscapeContext context;
        private readonly IDsaStep diamondStep;
        private readonly IDsaStep squareStep;

        public Dsa(LandscapeContext context, IDsaStep diamondStep, IDsaStep squareStep)
        {
            this.context = context;
            this.diamondStep = diamondStep;
            this.squareStep = squareStep;
        }

        public void Execute()
        {
            var iterations = (int)Mathf.Log(context.Length - 1, 2);
            for (int i = 1; i <= iterations; i++)
            {
                diamondStep.Execute(i);
                squareStep.Execute(i);
            }
        }
    }
}