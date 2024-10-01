using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class PolynomHorner : Algorithm
    {

        // методом Горнера представление полинома
        public static double PolynomialValueHorner(int[] v, double x)
        {
            double result = v[v.Length - 1];
            for (int k = v.Length - 2; k >= 0; k--)
            {
                result = v[k] + x * result;
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


                PolynomialValueHorner(vector, 2.0);


                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

    }
}
