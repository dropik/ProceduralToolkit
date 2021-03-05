namespace ProceduralToolkit.Models
{
    public class DSASettings
    {
        public int Seed { get; set; }
        public int Iterations { get; set; }
        public float Magnitude { get; set; }
        public float Hardness { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DSASettings settings &&
                   Seed == settings.Seed &&
                   Iterations == settings.Iterations &&
                   Magnitude == settings.Magnitude &&
                   Hardness == settings.Hardness;
        }

        public override int GetHashCode()
        {
            int hashCode = 1593229869;
            hashCode = hashCode * -1521134295 + Seed.GetHashCode();
            hashCode = hashCode * -1521134295 + Iterations.GetHashCode();
            hashCode = hashCode * -1521134295 + Magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Hardness.GetHashCode();
            return hashCode;
        }
    }
}