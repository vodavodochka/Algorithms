using System;
using System.Collections.Generic;
using System.Diagnostics;


using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lab1
{
    public class RadixSort : Algorithm
    {
        public override void MeasureIteration(int iterations)
        {
            Random rand = new Random();

            for (int n = 1; n <= iterations; n++)
            {
                int[] vector = GenerateVector(n, rand);
                stopwatch.Restart();
                RadixSorting(vector, 10); // Предполагаем десятичную систему
                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

        public int[] GenerateVector(int size, Random rand)
        {
            int[] vector = new int[size];

            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = rand.Next(1000000); // Генерация чисел до 1,000,000
            }
            return vector;
        }

        public static int GetLength(int[] arr)
        {
            int maxLength = 0;
            foreach (int num in arr)
            {
                int length = num.ToString().Length;
                if (length > maxLength)
                {
                    maxLength = length;
                }
            }
            return maxLength;
        }

        public static int[] RadixSorting(int[] arr, int range)
        {
            int length = GetLength(arr);
            List<int>[] lists = new List<int>[range];
            for (int i = 0; i < range; ++i)
                lists[i] = new List<int>();

            for (int step = 0; step < length; ++step)
            {
                // Распределение по спискам
                for (int i = 0; i < arr.Length; ++i)
                {
                    int temp = (arr[i] % (int)Math.Pow(range, step + 1)) / (int)Math.Pow(range, step);
                    lists[temp].Add(arr[i]);
                }

                // Сборка
                int k = 0;
                for (int i = 0; i < range; ++i)
                {
                    for (int j = 0; j < lists[i].Count; ++j)
                    {
                        arr[k++] = lists[i][j];
                    }
                    lists[i].Clear();
                }
            }
            return arr;
        }
    }
}
