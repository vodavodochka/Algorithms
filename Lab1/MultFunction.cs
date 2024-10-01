using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class MultFunction : Algorithm
    {
        // Произведение элементов
        public static int ProductFunction(int[] v)
        {
            int product = 1;
            foreach (var item in v)
            {
                product *= item;
            }
            return product;
        }

        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Генерация случайного вектора
                int[] vector = Vector.GenerateRandomVector(n);

                // Вызов функции для тестирования
                ProductFunction(vector);

                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }
    }
}
