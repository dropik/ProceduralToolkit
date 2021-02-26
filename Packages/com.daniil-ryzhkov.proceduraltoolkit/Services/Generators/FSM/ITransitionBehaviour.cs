using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface ITransitionBehaviour
    {
        IEnumerable<Vector3> MoveNext(Vector3 vertex);
        ITransitionBehaviour SetDefaultNext(ITransitionBehaviour next);
        ITransitionBuilder On(Func<bool> condition);
        ITransitionBehaviour DoNotZeroColumn();
    }
}
