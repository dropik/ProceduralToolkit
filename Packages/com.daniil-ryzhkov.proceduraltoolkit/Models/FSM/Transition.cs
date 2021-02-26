using System;

namespace ProceduralToolkit.Models.FSM
{
    public sealed class Transition
    {
        public Func<bool> Condition { get; set; }
        public string NextState { get; set; }
        public bool ZeroColumn { get; set; } = true;
    }
}
