using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
    public interface IGeneratorBootProvider
    {
        GeneratorBoot GetGeneratorBoot(BaseShapeGeneratorSettings baseShape);
    }
}
