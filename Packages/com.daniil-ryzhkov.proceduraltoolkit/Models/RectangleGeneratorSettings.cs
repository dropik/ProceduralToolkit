using System;

namespace ProceduralToolkit.Models
{
    public class RectangleGeneratorSettings
    {
        public float Length { get; set; }
        public float Width { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RectangleGeneratorSettings settings &&
                   Length == settings.Length &&
                   Width == settings.Width;
        }

        public override int GetHashCode()
        {
            int hashCode = -1135836612;
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            return hashCode;
        }
    }
}