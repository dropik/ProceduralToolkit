using System;

namespace ProceduralToolkit
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LayoutPathAttribute : Attribute
    {
        public string Path { get; set; }

        public LayoutPathAttribute(string path)
        {
            Path = path;
        }
    }
}
