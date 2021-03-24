namespace ProceduralToolkit.Models
{
    public class DsaSettings
    {
        public int Seed { get; set; }
        public float Magnitude { get; set; }
        public float Hardness { get; set; }
        public float Bias { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DsaSettings settings &&
                   Seed == settings.Seed &&
                   Magnitude == settings.Magnitude &&
                   Hardness == settings.Hardness &&
                   Bias == settings.Bias;
        }

        public override int GetHashCode()
        {
            int hashCode = -228455935;
            hashCode = hashCode * -1521134295 + Seed.GetHashCode();
            hashCode = hashCode * -1521134295 + Magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Hardness.GetHashCode();
            hashCode = hashCode * -1521134295 + Bias.GetHashCode();
            return hashCode;
        }
    }
}