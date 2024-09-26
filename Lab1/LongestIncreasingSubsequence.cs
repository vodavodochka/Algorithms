using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class LongestIncreasingSubsequence : Algorithm
    {
        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Симуляция выполнения итерации
                FindSubsequence(n);

                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

        public static void FindSubsequence(int n)
        {
            int[] sequence = new int[n];
        }

        public static void SequenceFill(int[] a)
        {
            Random rand = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rand.Next();
            }
        }
    }
}
