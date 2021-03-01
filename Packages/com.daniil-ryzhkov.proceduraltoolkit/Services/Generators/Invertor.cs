using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Invertor : WithInputSuffixGenerator
    {
        public Invertor(Func<int, IMachine> machineProvider) : base(machineProvider) { }

        protected override IEnumerable<Vector3> InputSuffix
        {
            get
            {
                for (int i = 0; i < ColumnsInRow - 1; i++)
                {
                    yield return default;
                }
            }
        }
    }
}
