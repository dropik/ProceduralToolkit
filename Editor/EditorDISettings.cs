using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralToolkit.Internal;

namespace ProceduralToolkit
{
    [ExecuteInEditMode]
    public class EditorDISettings : MonoBehaviour
    {
        public List<IContainer> Containers { get; set; }
        public float InjectionIdleTime { get; set; } = 1f;
        public List<Transform> containers;

        private void Awake()
        {
            RunCleanInject();
        }

        private void Start()
        {
            StartCoroutine(InjectionCoroutine());
        }

        private IEnumerator InjectionCoroutine()
        {
            while (true)
            {
                RunInject();
                yield return new WaitForSeconds(InjectionIdleTime);
            }
        }

        private void OnValidate()
        {
            RunCleanInject();
        }

        private void RunCleanInject()
        {
            RunInjectAction(container => container?.CleanInject());
        }

        private void RunInject()
        {
            RunInjectAction(container => container?.Inject());
        }

        private void RunInjectAction(Action<IContainer> action)
        {
            Containers?.ForEach(action);
        }
    }
}
