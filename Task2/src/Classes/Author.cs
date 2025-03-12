using System;
using System.Collections.Generic;

namespace Task2
{
    public class Author : Person
    {
        public string Biography { get; set; }

        public Author(string name, DateTime birthDate, string biography) : base(name, birthDate)
        {
            Biography = biography;
        }

        public override string DisplayInfo()
        {
            return $"Автор: {Name}, Рожденный: {BirthDate.ToShortDateString()}, Биография: {Biography}";
        }
    }
}
