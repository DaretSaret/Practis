using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadSafety_2_
{
    public class Program
    {
        static int counter = 0;

        private static object protector = new object();

        static void Main()
        {

            Thread thread = new Thread(new ThreadStart(Together));
            Thread thread1 = new Thread(new ThreadStart(Together));
            thread.Start();
            thread1.Start();
            thread.Join();
            thread1.Join();

            Console.WriteLine(counter);
        }
        static void Together()
        {
            lock (protector)
            {
                for (int i = 0; i < 100000; i++) //изменил на 100к ибо у меня не было ошибок в подсчете
                {
                    counter++;
                }
            }
        }
    }
}
