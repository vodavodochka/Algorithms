using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class PowerSet : Algorithm
    {
        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Генерация случайного вектора
                int[] vector = Vector.GenerateRandomVector(n);

                // Вызов функций для тестирования

                List<List<int>> result = GeneratePowerSet(vector);

                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }



        public static List<List<int>> GeneratePowerSet(int[] vector)
        {
            List<List<int>> powerSet = new List<List<int>>();
            int n = vector.Length;
            int totalSubsets = 1 << n; // 2^n

            for (int i = 0; i < totalSubsets; i++)
            {
                List<int> subset = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    // Проверяем, включен ли j-й элемент в текущее подмножество
                    if ((i & (1 << j)) != 0)
                    {
                        subset.Add(vector[j]);
                    }
                }
                powerSet.Add(subset);
            }

            return powerSet;
        }
    }

}
