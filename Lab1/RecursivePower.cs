using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class RecursivePower : AlgorithmWithSteps
    {
        int f = 0;
        public override void MeasureIteration(int iterations)
        {
            int[] vector = GenerateVector(iterations);

            for (int i = 0; i < iterations; i++)
            {
                steps = 0;

                PowerRecursive(2, vector[i]);
                iterationData.Add(new IterationData { IterationNumber = i, TimeSpent = steps });
            }
        }

        public int[] GenerateVector(int iterations)
        {
            int[] vector = new int[iterations];
            for (int i = 0; i < iterations; i++)
            {
                vector[i] = i + 1;
            }
            return vector;
        }

        private int PowerRecursive(int baseNumber, int exponent)
        {
            
            if (exponent == 0)
            {
                f = 1;
                steps += 3;
                return f;
            }
            steps++;
            f = PowerRecursive(baseNumber, exponent / 2);
            if ((exponent % 2) == 1)
            {
                f = f * f * baseNumber;
            }
            else
            {
                f *= f;
            }
            steps += 5;
            return f;
        }

    }
}
