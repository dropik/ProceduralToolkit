using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Displacer : IDisplacer
    {
        private readonly DsaSettings settings;

        public Displacer(DsaSettings settings)
        {
            this.settings = settings;
            Random.InitState(settings.Seed);
        }

        public float GetDisplacement(int iteration)
        {
            var magnitude = Mathf.Pow(2, -1 * iteration * settings.Hardness) * settings.Magnitude;
            return Random.Range(-magnitude, magnitude);
        }
    }
}