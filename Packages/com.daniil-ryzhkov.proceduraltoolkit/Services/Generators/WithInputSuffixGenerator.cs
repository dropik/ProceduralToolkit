using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class WithInputSuffixGenerator : FSMBasedGenerator
    {
        protected WithInputSuffixGenerator(Func<int, IMachine> machineProvider) : base(machineProvider) { }

        public override IEnumerable<Vector3> InputVertices
        {
            get => base.InputVertices;
            set => base.InputVertices = value.Concat(InputSuffix);
        }

        protected abstract IEnumerable<Vector3> InputSuffix { get; }
    }
}
