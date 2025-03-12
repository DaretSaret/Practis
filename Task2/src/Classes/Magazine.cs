using System;
using System.Collections.Generic;

namespace Task2
{
    public class Magazine : Task2.Item, Task2.ILendable
    {
        public int IssueNumber { get; set; }

        public void Lend()
        {
            Console.WriteLine($"Журнал '{Title}' выдан.");
        }

        public void Return()
        {
            Console.WriteLine($"Журнал '{Title}' возвращен.");
        }
    }
}
