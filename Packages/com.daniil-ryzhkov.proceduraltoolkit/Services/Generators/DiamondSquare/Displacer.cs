using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class Displacer : IDisplacer
    {
        private readonly DsaSettings settings;

        public Displacer(DsaSettings settings)
        {
            this.settings = settings;
        }

        public float GetNormalizedDisplacement(int iteration)
        {
            var magnitude = Mathf.Pow(2, -1 * iteration * settings.Hardness) / 2;
            return Random.Range(-magnitude, magnitude);
        }
    }
}