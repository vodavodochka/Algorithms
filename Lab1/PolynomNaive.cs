using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class PolynomNaive : Algorithm
    {
        // Вычисление значения полинома
        public static double PolynomialValueNaive(int[] v, double x)
        {
            double result = 0;
            for (int k = 0; k < v.Length; k++)
            {
                result += v[k] * Math.Pow(x, k);
            }
            return result;
        }

        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Генерация случайного вектора
                int[] vector = Vector.GenerateRandomVector(n);

                // Вызов функций для тестирования


                PolynomialValueNaive(vector, 2.0);


                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

    }
}