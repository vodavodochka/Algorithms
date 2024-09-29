using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class FastPower : AlgorithmWithSteps
    {
        public override void MeasureIteration(int exponent)
        {
            int baseNumber = 2;
            int result = 1;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (exponent > 0)
            {
                steps++; 
                if ((exponent & 1) == 1) result *= baseNumber;
                baseNumber *= baseNumber;
                exponent >>= 1;

                iterationData.Add(new IterationData
                {
                    IterationNumber = steps,
                    TimeSpent = stopwatch.Elapsed.TotalMilliseconds
                });
            }

            stopwatch.Stop();
        }
    }
}
