using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
    public class GeneratorBootDescriptor : ScriptableObject
    {
        public ScriptableObject baseShape;
        public List<ScriptableObject> filters;
    }
}