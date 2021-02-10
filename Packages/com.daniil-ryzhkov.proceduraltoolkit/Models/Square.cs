namespace ProceduralToolkit.Models
{
    public class Square
    {
        public Square(int index1, int index2, int index3, int index4)
        {
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
            Index4 = index4;
        }

        public int Index1 { get; set; }
        public int Index2 { get; set; }
        public int Index3 { get; set; }
        public int Index4 { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Square square &&
                   Index1 == square.Index1 &&
                   Index2 == square.Index2 &&
                   Index3 == square.Index3 &&
                   Index4 == square.Index4;
        }

        public override int GetHashCode()
        {
            int hashCode = 1674397662;
            hashCode = hashCode * -1521134295 + Index1.GetHashCode();
            hashCode = hashCode * -1521134295 + Index2.GetHashCode();
            hashCode = hashCode * -1521134295 + Index3.GetHashCode();
            hashCode = hashCode * -1521134295 + Index4.GetHashCode();
            return hashCode;
        }
    }
}
