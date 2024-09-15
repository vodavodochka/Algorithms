using System;
using Lab1;

namespace Lab1
{
    public class MatrixOperations
    {
        public static void MatrixMultiplication(int n)
        {
            float[,] matrix1 = new float[n, n];
            float[,] matrix2 = new float[n, n];

            MatrixFill(matrix1, matrix2);

            float[,] result = new float[n, n];
            MatrixMultiply(matrix1, matrix2, result);
        }

        public static void MatrixFill(float[,] a, float[,] b)
        {
            Random rand = new Random();
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
    }
}