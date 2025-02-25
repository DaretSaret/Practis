using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Task_3_
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            await Asynchronnoe();
            stopwatch.Stop();
            Console.WriteLine("асинхронное - " + stopwatch.Elapsed.TotalSeconds + " сек");

            stopwatch.Restart();
            Posledovatelnoe();
            stopwatch.Stop();
            Console.WriteLine("последовательное - " + stopwatch.Elapsed.TotalSeconds + " сек");
        }
        static async Task Asynchronnoe()
        {
            Task task1 = Task.Run(async () =>
            {
                Console.WriteLine("Первый таск запущен!");
                await Task.Delay(2000);
            });

            Task task2 = Task.Run(async () =>
            {
                Console.WriteLine("Второй таск запущен!");
                await Task.Delay(3000);
            });

            Task task3 = Task.Run(async () =>
            {
                Console.WriteLine("Третий таск запущен!");
                await Task.Delay(1000);
                throw new Exception("типо ошибка");
            });


            try
            {
                await Task.WhenAll(task1, task2, task3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static void Posledovatelnoe()
        {
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine("Первый последовательный таск запущен!");
                Task.Delay(1000).Wait();
            });
            task1.Wait();
            Task task2 = Task.Run(() =>
            {
                Console.WriteLine("Второй последовательный таск запущен!");
                Task.Delay(2000).Wait();
            });
            task2.Wait();
            Task task3 = Task.Run(() =>
            {
                Console.WriteLine("Третий последовательный таск запущен!");
                Task.Delay(3000).Wait();
            });
            task3.Wait();
        }
    }
}
