using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class QuickSort : Algorithm
    {

        // быстрая сортировка
        public static void Quick(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                Quick(array, left, pivotIndex - 1);
                Quick(array, pivotIndex + 1, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            int temp1 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp1;

            return i + 1;
        }
        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Генерация случайного вектора
                int[] vector = Vector.GenerateRandomVector(n);

                // Вызов функций для тестирования

                int[] vectorForQuickSort = (int[])vector.Clone();
                Quick(vectorForQuickSort, 0, vectorForQuickSort.Length - 1);

                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }
    }
}
