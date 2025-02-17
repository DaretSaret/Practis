using System;
using System.Diagnostics;

namespace Task1.Sorts
{
    public class BubbleSort : SortingAlgorithm
    {
        public override SortingResult Sort(int[] array)
        {
            var stopwatch = Stopwatch.StartNew();
            int n = array.Length;
            int[] sortedArray = (int[])array.Clone();

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (sortedArray[j] > sortedArray[j + 1])
                    {
                        // Меняем местами
                        int temp = sortedArray[j];
                        sortedArray[j] = sortedArray[j + 1];
                        sortedArray[j + 1] = temp;
                    }
                }
            }

            stopwatch.Stop();
            return new SortingResult { ExecutionTime = stopwatch.Elapsed, SortedArray = sortedArray };
        }
    }
}
