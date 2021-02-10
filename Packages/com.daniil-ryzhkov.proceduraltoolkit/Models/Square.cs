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
    }
}
