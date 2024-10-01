using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Bubble : Algorithm
    {
        // пузырёк
        public static void BubbleSort(int[] v)
        {
            int n = v.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (v[j] > v[j + 1])
                    {
                        int temp = v[j];
                        v[j] = v[j + 1];
                        v[j + 1] = temp;
                    }
                }
            }
        }

        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Генерация случайного вектора
                int[] vector = Vector.GenerateRandomVector(n);

                // Вызов функций для тестирования

                BubbleSort(vector);

                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

    }
}
