using System;
using System.Collections.Generic;

namespace Task2
{
    public class Reader : Person
    {
        public List<Loan> LoanedBooks { get; set; } = new List<Loan>();

        public Reader(string name, DateTime birthDate) : base(name, birthDate)
        {
        }

        public override string DisplayInfo()
        {
            return $"Читатель: {Name}, Рожденный: {BirthDate.ToShortDateString()}";
        }
    }
}
