using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Security.Cryptography.Core;

namespace Lab1
{
    internal class LinearSearchAlgoritm : Algorithm
    {
        private int[] array;

        public override void MeasureIteration(int arraySize)
        {
            array = GenerateRandomArrays(arraySize);

            int target = array[new Random().Next(array.Length)];

            stopwatch.Start();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    stopwatch.Stop();
                    iterationData.Add(new IterationData
                    {
                        IterationNumber = i + 1,
                        TimeSpent = stopwatch.Elapsed.TotalMilliseconds
                    });
                    break;
                }

                iterationData.Add(new IterationData
                {
                    IterationNumber = i + 1,
                    TimeSpent = stopwatch.Elapsed.TotalMilliseconds
                });

                stopwatch.Restart();
            }
            stopwatch.Stop();  
        }

        private int[] GenerateRandomArrays(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for(int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 100);
            }
            return array;
        }

    }
}
