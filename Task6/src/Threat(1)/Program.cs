using System;
using System.Diagnostics;
using System.Threading;

namespace Task6
{
    public class Program
    {

        static void Main()
        {
            DateTime startTime = DateTime.Now;
            Thread thread = new Thread(new ThreadStart(ThreadOne));
            thread.Start();
            thread.Join();
            Thread thread1 = new Thread(new ThreadStart(ThreadTwo));
            thread1.Start();
            thread1.Join();
            Thread thread2 = new Thread(new ThreadStart(ThreadThree));
            thread2.Start();
            thread2.Join();

            DateTime endTime = DateTime.Now;
            TimeSpan executionTime = endTime - startTime;

            Console.WriteLine("Время выполнения: " + (executionTime.TotalSeconds) + " секунд");
        }

        static void ThreadOne()
        {
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("First поток - итерация - " + i);
                Thread.Sleep(500);
            }
        }
        static void ThreadTwo()
        {
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("Second поток - итерация - " + i);
                Thread.Sleep(500);
            }
        }
        static void ThreadThree()
        {
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("Third поток - итерация - " + i);
                Thread.Sleep(500);
            }
        }
    }
}
