using System;
using System.Diagnostics;
using Task1.Sorts;

namespace Task1
{

    public struct SortingResult
    {
        public TimeSpan ExecutionTime;
        public int[] SortedArray;
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов для сортировки (например, 100, 1000, 10000):");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Некорректный ввод. Программа завершена.");
                return;
            }

            int[] data = GenerateRandomArray(size);
            SortingAlgorithm[] algorithms = new SortingAlgorithm[]
            {
                new BubbleSort(),
                new InsertionSort(),
                new QuickSort(),
                new MergeSort()
            };

            foreach (var algorithm in algorithms)
            {
                var result = algorithm.Sort(data);
                Console.WriteLine($"{algorithm.GetType().Name}: Время выполнения: {result.ExecutionTime} мс");
            }
        }

        static int[] GenerateRandomArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 10000);
            }
            return array;
        }
    }
}
