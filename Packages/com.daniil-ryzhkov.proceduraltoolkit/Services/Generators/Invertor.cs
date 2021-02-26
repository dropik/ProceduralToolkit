using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Invertor : FSMBasedGenerator
    {
        public Invertor(Func<int, IMachine> machineProvider) : base(machineProvider) { }

        public override IEnumerable<Vector3> InputVertices
        {
            get => base.InputVertices;
            set => base.InputVertices = value.Concat(InputSuffix);
        }

        private IEnumerable<Vector3> InputSuffix
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
