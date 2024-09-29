using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public abstract class AlgorithmWithSteps
    {
        protected List<IterationData> iterationData;
        protected int steps;

        public AlgorithmWithSteps()
        {
            iterationData = new List<IterationData>();
            int steps = 0;
        }

        public abstract void MeasureIteration(int iterations, int baseNumber);

        public void Start(int iterations, int baseNumber)
        {
            iterationData.Clear();
            MeasureIteration(iterations, baseNumber);
        }

        public List<IterationData> GetIterationData()
        {
            return iterationData;
        }
    }
}
