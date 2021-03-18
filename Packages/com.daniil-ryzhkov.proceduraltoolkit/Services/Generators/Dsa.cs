using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Dsa : IDsa
    {
        private readonly DsaSettings settings;
        private readonly LandscapeContext context;
        private readonly IDsaStep diamondStep;
        private readonly IDsaStep squareStep;

        public Dsa(DsaSettings settings, LandscapeContext context, IDsaStep diamondStep, IDsaStep squareStep)
        {
            this.settings = settings;
            this.context = context;
            this.diamondStep = diamondStep;
            this.squareStep = squareStep;
        }

        public void Execute()
        {
            SetupInitialValues();
            ExecuteSteps();
        }

        private void SetupInitialValues()
        {
            Random.InitState(settings.Seed);
            CopySettingsToContext();
            CalculateLength();
            context.Vertices = CreateVerticesBuffer();
            CalculateGridSize();
        }

        private void ExecuteSteps()
        {
            for (int i = 1; i <= context.Iterations; i++)
            {
                diamondStep.Execute(i);
                squareStep.Execute(i);
            }
        }

        private void CopySettingsToContext()
        {
            context.Iterations = settings.Resolution;
            context.SideLength = settings.SideLength;
        }

        private void CalculateLength()
        {
            context.Length = (int)Mathf.Pow(2, context.Iterations) + 1;
        }

        private Vector3[] CreateVerticesBuffer()
        {
            var vertices = AllocateVerticesBuffer();
            SetCornerVertices(vertices);
            return vertices;
        }

        private void CalculateGridSize()
        {
            context.GridSize = (context.Vertices[context.Length * context.Length - 1] - context.Vertices[0]) / (context.Length - 1);
        }

        private Vector3[] AllocateVerticesBuffer()
            => new Vector3[context.Length * context.Length];

        private void SetCornerVertices(Vector3[] vertices)
        {
            var halfSide = context.SideLength / 2;
            vertices[0] = new Vector3(-halfSide, 0, halfSide);
            vertices[context.Length - 1] = new Vector3(halfSide, 0, halfSide);
            vertices[(context.Length - 1) * context.Length] = new Vector3(-halfSide, 0, -halfSide);
            vertices[context.Length * context.Length - 1] = new Vector3(halfSide, 0, -halfSide);
        }
    }
}