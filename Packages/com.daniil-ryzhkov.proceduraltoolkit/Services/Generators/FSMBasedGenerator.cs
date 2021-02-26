using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class FSMBasedGenerator : ColumnsBasedGenerator
    {
        private readonly Func<int, FSMContext> contextProvider;
        private readonly Func<int, IMachine> machineProvider;

        public FSMBasedGenerator(Func<int, FSMContext> contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public FSMBasedGenerator(Func<int, IMachine> machineProvider)
        {
            this.machineProvider = machineProvider;
        }

        public override IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var machine = machineProvider.Invoke(ColumnsInRow);
                foreach (var vertex in InputVertices)
                {
                    foreach (var newVertex in machine.MoveNext(vertex))
                    {
                        yield return newVertex;
                    }
                }
            }
        }
    }
}