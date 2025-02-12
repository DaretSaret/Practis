using System;
using System.Diagnostics;

namespace Task1.Sorts
{
    public class InsertionSort : SortingAlgorithm
    {
        public override SortingResult Sort(int[] array)
        {
            var stopwatch = Stopwatch.StartNew();
            int[] sortedArray = (int[])array.Clone();
            int n = sortedArray.Length;

            for (int i = 1; i < n; i++)
            {
                int key = sortedArray[i];
                int j = i - 1;

                while (j >= 0 && sortedArray[j] > key)
                {
                    sortedArray[j + 1] = sortedArray[j];
                    j--;
                }
                sortedArray[j + 1] = key;
            }

            stopwatch.Stop();
            return new SortingResult { ExecutionTime = stopwatch.Elapsed, SortedArray = sortedArray };
        }
    }
}
