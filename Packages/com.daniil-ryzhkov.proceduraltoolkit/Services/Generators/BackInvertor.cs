using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class BackInvertor : WithInputSuffixGenerator
    {
        public BackInvertor(Func<int, IMachine> machineProvider) : base(machineProvider) { }

        protected override IEnumerable<Vector3> InputSuffix
        {
            get
            {
                for (int i = 0; i < ColumnsInRow; i++)
                {
                    yield return default;
                }
            }
        }
    }
}
