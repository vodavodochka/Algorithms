using System;

namespace Lab1
{
    public class MatrixMultiplication : Algorithm
    {

        private static Random rand = new Random();

        public override void MeasureIteration(int iterations)
        {
            for (int n = 1; n <= iterations; n++)
            {
                stopwatch.Restart();

                
                MatrixMulti(n);

                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

        public static void MatrixMulti(int n)
        {   
            if (n > 2)
            {
                float[,] matrix1 = new float[n, n];
                float[,] matrix2 = new float[n, n];

                MatrixFill(matrix1, matrix2);

                float[,] result = new float[n, n];
                MatrixMultiply(matrix1, matrix2, result);
            }
            else
            {
                float[] matrix1 = new float[n];
                float[] matrix2 = new float[n];

                MatrixFill(matrix1, matrix2);

                float[] result = new float[n];
                MatrixMultiply(matrix1, matrix2, result);
            }
            
        }

        public static void MatrixFill(float[] a, float[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = (float)rand.NextDouble();
                b[i] = (float)rand.NextDouble();
            }
        }

        public static void MatrixFill(float[,] a, float[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = (float)rand.NextDouble();
                    b[i, j] = (float)rand.NextDouble();
                }
            }
        }

        public static void MatrixMultiply(float[,] a, float[,] b, float[,] result)
        {
            int n = a.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
        }

        public static void MatrixMultiply(float[] a, float[] b, float[] result)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i] * b[i];
            }
        }
    }
}
