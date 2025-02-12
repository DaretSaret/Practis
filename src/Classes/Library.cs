using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class Library
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Reader> Readers { get; set; } = new List<Reader>();
        public List<Loan> Loans { get; set; } = new List<Loan>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void AddReader(Reader reader)
        {
            Readers.Add(reader);
        }

        public void LendBook(Reader reader, Book book, DateTime dueDate)
        {
            if (!book.IsAvailable)
            {
                Console.WriteLine("Книга недоступна.");
                return;
            }

            book.IsAvailable = false;
            Loan loan = new Loan(reader, book, DateTime.Now, dueDate);
            Loans.Add(loan);
            reader.LoanedBooks.Add(loan);

            Console.WriteLine($"Книга '{book.Title}' выдана читателю '{reader.Name}'.");
        }

        public void ReturnBook(Reader reader, Book book)
        {
            Loan loan = reader.LoanedBooks.FirstOrDefault(l => l.Book == book && l.IsActive);
            if (loan != null)
            {
                loan.IsActive = false;
                book.IsAvailable = true;
                reader.LoanedBooks.Remove(loan);
                Console.WriteLine($"Книга '{book.Title}' возвращена читателем '{reader.Name}'.");
            }
            else
            {
                Console.WriteLine("Данная книга не числится за читателем.");
            }
        }
    }
}
