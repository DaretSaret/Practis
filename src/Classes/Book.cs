using System;
using System.Collections.Generic;

namespace Task2
{
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, Author author, int publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            IsAvailable = true;
        }
    }
}
