using System;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    public class Rectangle : MonoBehaviour, IGeneratorSettings
    {
        [Min(0)]
        public float length;
        [Min(0)]
        public float width;

        public event Action Updated;

        public PlaneGeneratorSettings Settings => new PlaneGeneratorSettings()
        {
            Length = length,
            Width = width
        };

        public void Reset()
        {
            length = 1;
            width = 1;
        }

        public void OnValidate()
        {
            Updated?.Invoke();
        }
    }
}