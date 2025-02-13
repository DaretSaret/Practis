using System;

namespace Task3.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return $"Название: {Title}, Автор: {Author}, ISBN: {ISBN}, Год: {Year}, Создана: {CreateDate}";
        }
    }
}
