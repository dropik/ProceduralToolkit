﻿using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnDiamond : BaseDiamondTilingState
    {
        public ReturnDiamond(FSMContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex)
        {
            return new Vector3(vertex.x, 0, vertex.z) + Context.DiamondTilingContext.XZShift;
        }
    }
}