using System;
using System.Collections.Generic;
using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class BaseDiamondGenerator
    {
        protected Func<IEnumerable<Vector3>, int, FSMContext> ContextProvider { get; private set; }

        public BaseDiamondGenerator(Func<IEnumerable<Vector3>, int, FSMContext> contextProvider)
        {
            this.ContextProvider = contextProvider;
        }

        public IEnumerable<Vector3> InputVertices
        {
            get => inputVertices ?? (new Vector3[0]);
            set => inputVertices = value;
        }
        private IEnumerable<Vector3> inputVertices;

        public int ColumnsInRow
        {
            get => columnsInRow;
            set => columnsInRow = value < 0 ? 0 : value;
        }
        private int columnsInRow;

        public abstract IEnumerable<Vector3> OutputVertices { get; }
    }
}
