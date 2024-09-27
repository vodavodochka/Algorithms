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

        public abstract void MeasureIteration(int iterations);

        public void Start(int iterations)
        {
            iterationData.Clear();
            MeasureIteration(iterations);
        }

        public List<IterationData> GetIterationData()
        {
            return iterationData;
        }
    }
}
