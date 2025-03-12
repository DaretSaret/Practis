using System;
using System.Collections.Generic;

namespace Task2
{
    public class Loan
    {
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Loan(Reader reader, Book book, DateTime loanDate, DateTime dueDate)
        {
            Reader = reader;
            Book = book;
            LoanDate = loanDate;
            DueDate = dueDate;
        }
    }
}
