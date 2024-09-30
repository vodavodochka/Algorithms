using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    //Итеративный алгоритм возведения в степень
    public class PowerAlgoritmms : AlgorithmWithSteps
    {
        public override void MeasureIteration(int exponent)
        {
            int baseNumber = 2;
            int result = 1;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < exponent; i++)
            {
                result *= baseNumber;
                steps++;  
                iterationData.Add(new IterationData
                {
                    IterationNumber = i + 1,
                    TimeSpent = stopwatch.Elapsed.TotalMilliseconds
                });
            }

            stopwatch.Stop();
        }
    }
}
