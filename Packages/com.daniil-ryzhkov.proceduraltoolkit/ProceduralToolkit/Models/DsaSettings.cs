using System;
using UnityEngine;

namespace ProceduralToolkit.Models
{
    [Serializable]
    public class DsaSettings
    {
        [Min(0)]
        public int seed = 0;

        [Range(0, 1)]
        public float magnitude = 1;

        [Range(0, 2)]
        public float hardness = 1;

        [Range(0, 1)]
        public float bias = 0.5f;

        public override bool Equals(object obj)
        {
            return obj is DsaSettings settings &&
                   seed == settings.seed &&
                   magnitude == settings.magnitude &&
                   hardness == settings.hardness &&
                   bias == settings.bias;
        }

        public override int GetHashCode()
        {
            int hashCode = -228455935;
            hashCode = hashCode * -1521134295 + seed.GetHashCode();
            hashCode = hashCode * -1521134295 + magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + hardness.GetHashCode();
            hashCode = hashCode * -1521134295 + bias.GetHashCode();
            return hashCode;
        }
    }
}