using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class LinearSearchAlgoritm : Algorithm
    {
        public override void MeasureIteration(int iterations)
        {
            Random random = new Random();

            for (int i = 0; i < iterations; i++)
            {
                stopwatch.Restart();

                int[] array = GenerateRandomArrays(i + 1, random);
                int target = array[random.Next(array.Length)];

                stopwatch.Stop();

                iterationData.Add(new IterationData
                {
                    IterationNumber = i + 1,
                    TimeSpent = stopwatch.Elapsed.TotalMilliseconds
                });
            }
        }

        private int[] GenerateRandomArrays(int size, Random random)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 100);
            }
            return array;
        }


    }
}
