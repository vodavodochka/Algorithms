namespace Lab1
{
    public class Timsort : Algorithm
    {
        public override void MeasureIteration(int iterations)
        {
            Random rand = new Random();
            for (int n = 1; n <= iterations; n++)
            {
                int[] vector = GenerateVector(n, rand);
                stopwatch.Restart();
                TimSorting(vector, n);
                stopwatch.Stop();
                iterationData.Add(new IterationData { IterationNumber = n, TimeSpent = stopwatch.Elapsed.TotalSeconds });
            }
        }

        public int[] GenerateVector(int arraySize, Random rand)
        {
            int[] vector = new int[arraySize];
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = rand.Next(1000);
            }
            return vector;
        }

        private static int GetMinrun(int n)
        {
            int r = 0;
            while (n >= 64)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
        }

        public static int[] TimSorting(int[] arr, int n)
        {
            int minRun = GetMinrun(n);

            for (int i = 0; i < n; i += minRun)
            {
                arr = InsertionSort(arr, i, Math.Min((i + minRun - 1), (n - 1)));
            }

            for (int size = minRun; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    if (mid < right)
                        arr = Merge(arr, left, mid, right);
                }
            }
            return arr;
        }

        private static int[] InsertionSort(int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
            return arr;
        }

        public static int[] Merge(int[] arr, int l, int m, int r)
        {
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int x = 0; x < len1; x++)
            {
                left[x] = arr[l + x];
            }
            for (int x = 0; x < len2; x++)
            {
                right[x] = arr[m + 1 + x];
            }

            int i = 0;
            int j = 0;
            int k = l;

            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }
            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
            return arr;
        }
    }
}