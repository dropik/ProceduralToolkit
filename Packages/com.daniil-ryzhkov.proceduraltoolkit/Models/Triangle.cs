namespace ProceduralToolkit.Models
{
    public class Triangle
    {
        public Triangle(int index1, int index2, int index3)
        {
            Index1 = index1;
            Index2 = index2;
            Index3 = index3;
        }

        public int Index1 { get; set; }
        public int Index2 { get; set; }
        public int Index3 { get; set; }
    }
}