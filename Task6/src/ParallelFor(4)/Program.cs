using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelFor_4_
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = new ConcurrentDictionary<int, long>();
            var stopwatch = Stopwatch.StartNew();
            Parallel.For(1, 11, i =>
            {
                long factorial = Factorial(i);
                results[i] = factorial;
                Console.WriteLine("Параллельный запуск: факториал - " +(i)+ " " + (factorial));
            });
            stopwatch.Stop();
            Console.WriteLine("параллельный занял - " + stopwatch.Elapsed.TotalSeconds + " сек");

            results.Clear();
            stopwatch.Restart();
            for (int i = 1; i <= 10; i++)
            {
                long factorial = Factorial(i);
                results[i] = factorial;
                Console.WriteLine("обычный запуск: факториал - " + (i) + " " + (factorial));
            }
            stopwatch.Stop();
            Console.WriteLine("обычный занял - " + stopwatch.Elapsed.TotalSeconds + " сек");
        }
        public static long Factorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
