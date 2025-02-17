using System;
using System.Diagnostics;

namespace Task1.Sorts
{
    public class MergeSort : SortingAlgorithm
    {
        public override SortingResult Sort(int[] array)
        {
            var stopwatch = Stopwatch.StartNew();
            int[] sortedArray = (int[])array.Clone();
            MergeSortAlgorithm(sortedArray, 0, sortedArray.Length - 1);
            stopwatch.Stop();
            return new SortingResult { ExecutionTime = stopwatch.Elapsed, SortedArray = sortedArray };
        }

        private void MergeSortAlgorithm(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSortAlgorithm(array, left, middle);
                MergeSortAlgorithm(array, middle + 1, right);
                Merge(array, left, middle, right);
            }
        }

        private void Merge(int[] array, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            int[] L = new int[n1];
            int[] R = new int[n2];

            for (int i = 0; i < n1; i++)
                L[i] = array[left + i];
            for (int j = 0; j < n2; j++)
                R[j] = array[middle + 1 + j];

            int k = left;
            int iIndex = 0, jIndex = 0;

            while (iIndex < n1 && jIndex < n2)
            {
                if (L[iIndex] <= R[jIndex])
                {
                    array[k] = L[iIndex];
                    iIndex++;
                }
                else
                {
                    array[k] = R[jIndex];
                    jIndex++;
                }
                k++;
            }

            while (iIndex < n1)
            {
                array[k] = L[iIndex];
                iIndex++;
                k++;
            }

            while (jIndex < n2)
            {
                array[k] = R[jIndex];
                jIndex++;
                k++;
            }
        }
    }
}
