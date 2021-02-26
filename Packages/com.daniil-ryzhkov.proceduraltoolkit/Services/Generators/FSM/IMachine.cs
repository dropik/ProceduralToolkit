using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IMachine
    {
        void AddState(string name, Action<IStateBuilder> build);
        void SetState(string name);
        IEnumerable<Vector3> MoveNext(Vector3 vertex);
    }
}