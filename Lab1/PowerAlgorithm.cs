using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class PowerAlgorithm : AlgorithmWithSteps
    {
        public override void MeasureIteration(int exponent)
        {
            int baseNumber = 2;
            int result = 1;
            int steps = 0;
            steps += 2;
            
            for (int i = 0; i < exponent; i++)
            {
                result *= baseNumber;
                iterationData.Add(new IterationData { IterationNumber = i, TimeSpent = steps });
                steps += 3;
                iterationData.Add(new IterationData { IterationNumber = i, TimeSpent = steps });
            }
            
        }
    }

}
