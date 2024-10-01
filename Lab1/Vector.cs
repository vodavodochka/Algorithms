using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Vector : Algorithm
    {
        

       

        

        // генерация вектора
        public static int[] GenerateRandomVector(int n)
        {
            Random rand = new Random();
            int[] v = new int[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = rand.Next(0, 100); 
            }
            return v;
        }

        


        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                // Генерация случайного вектора
                int[] vector = GenerateRandomVector(n);


                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

    }
    
}
