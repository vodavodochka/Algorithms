using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            SequenceFill(sequence);

            int[] lis = new int[n];
            int[] prevIndex = new int[n];
            int maxLength = 1;

            for (int i = 0; i < n; i++)
            {
                lis[i] = 1;
                prevIndex[i] = -1;
            }

            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (sequence[i] > sequence[j] && lis[i] < lis[j] + 1)
                    {
                        lis[i] = lis[j] + 1;
                        prevIndex[i] = j;
                    }
                }
                if (lis[i] > maxLength)
                {
                    maxLength = lis[i];
                }
            }

            int[] lisSequence = new int[maxLength];
            int index = Array.IndexOf(lis, maxLength);
            for (int i = maxLength - 1; i >= 0; i--)
            {
                lisSequence[i] = sequence[index];
                index = prevIndex[index];
            }
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
