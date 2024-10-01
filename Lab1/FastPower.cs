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
        private int steps;

        public override void MeasureIteration(int iterations)
        {
            int baseNumber = 2;
            int[] vector = GenerateVector(iterations);

            for (int i = 0; i < iterations; i++)
            {
                steps = 0; // Reset steps for each iteration
                FastPow(vector[i], baseNumber);
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

        public void FastPow(int exponent, int baseNumber)
        {
            int result = 1;
            while (exponent > 0)
            {
                steps++;
                if ((exponent & 1) == 1)
                {
                    result *= baseNumber;
                    steps++;
                }
                steps++;
                baseNumber *= baseNumber;

                exponent >>= 1;
                steps += 2;
            }
        }
    }
}
