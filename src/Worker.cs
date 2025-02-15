using System;
using System.Collections.Generic;
using System.IO;

namespace Task4
{
    public class Worker
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string Grade { get; set; }

        public Worker(string name, int age, DateTime dateOfEmployment, string grade)
        {
            Name = name;
            Age = age;
            DateOfEmployment = dateOfEmployment;
            Grade = grade;
        }

        public override string ToString()
        {
            return $"{Name};{Age};{DateOfEmployment:yyyy-MM-dd};{Grade}";
        }
    }
}
