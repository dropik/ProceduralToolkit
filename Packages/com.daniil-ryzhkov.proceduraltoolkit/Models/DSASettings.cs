namespace ProceduralToolkit.Models
{
    public class DsaSettings
    {
        public int Seed { get; set; }
        public int Resolution { get; set; }
        public float Magnitude { get; set; }
        public float Hardness { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DsaSettings settings &&
                   Seed == settings.Seed &&
                   Resolution == settings.Resolution &&
                   Magnitude == settings.Magnitude &&
                   Hardness == settings.Hardness;
        }

        public override int GetHashCode()
        {
            int hashCode = -228455935;
            hashCode = hashCode * -1521134295 + Seed.GetHashCode();
            hashCode = hashCode * -1521134295 + Resolution.GetHashCode();
            hashCode = hashCode * -1521134295 + Magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Hardness.GetHashCode();
            return hashCode;
        }
    }
}