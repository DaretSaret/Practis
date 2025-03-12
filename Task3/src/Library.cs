using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Task3.Models;

namespace Task3
{
    public class Library
    {
        private static List<Book> Books { get; set; } = new List<Book>();
        private static readonly string filePath = "books.json";

        public static void AddBook()
        {
            Console.WriteLine("Введите название книги:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите автора:");
            string author = Console.ReadLine();

            Console.WriteLine("Введите ISBN:");
            string isbn = Console.ReadLine();

            Console.WriteLine("Введите год публикации:");
            string year = Console.ReadLine();

            Book newBook = new Book
            {
                Title = title,
                Author = author,
                ISBN = isbn,
                Year = year,
                CreateDate = DateTime.Now
            };

            Books.Add(newBook);
            Console.WriteLine("Книга добавлена");
        }

        public static void RemoveBook()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("Библиотека пустая.");
                return;
            }

            PrintBooks(); // Show list of books
            Console.WriteLine("Введите ISBN книги для удаления:");
            string isbnToRemove = Console.ReadLine();

            Book bookToRemove = Books.Find(book => book.ISBN == isbnToRemove);
            if (bookToRemove != null)
            {
                Books.Remove(bookToRemove);
                Console.WriteLine("Книга удалена");
            }
            else
            {
                Console.WriteLine("Книга с ISBN не найдена.");
            }
        }

        public static void UpdateBook()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("Библиотека пустая.");
                return;
            }
            PrintBooks();
            Console.WriteLine("Введите ISBN книги для обновления:");
            string isbnToUpdate = Console.ReadLine();

            Book bookToUpdate = Books.Find(book => book.ISBN == isbnToUpdate);
            if (bookToUpdate != null)
            {
                Console.WriteLine("Что вы хотите обновить?");
                Console.WriteLine("1. Название");
                Console.WriteLine("2. Автор");
                Console.WriteLine("3. ISBN");
                Console.WriteLine("4. Год");
                Console.WriteLine("5. Все поля");
                Console.Write("Введите ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите новое название:");
                        bookToUpdate.Title = Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Введите нового автора:");
                        bookToUpdate.Author = Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("Введите новый ISBN:");
                        bookToUpdate.ISBN = Console.ReadLine();
                        break;
                    case "4":
                        Console.WriteLine("Введите новый год:");
                        bookToUpdate.Year = Console.ReadLine();
                        break;
                    case "5":
                        Console.WriteLine("Введите новое название:");
                        bookToUpdate.Title = Console.ReadLine();
                        Console.WriteLine("Введите нового автора:");
                        bookToUpdate.Author = Console.ReadLine();
                        Console.WriteLine("Введите новый ISBN:");
                        bookToUpdate.ISBN = Console.ReadLine();
                        Console.WriteLine("Введите новый год:");
                        bookToUpdate.Year = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }

                Console.WriteLine("Книга обновлена");
            }
            else
            {
                Console.WriteLine("Книга с ISBN не найдена.");
            }
        }

        public static void PrintBook(string isbn)
        {
            Book foundBook = Books.Find(b => b.ISBN == isbn);
            if (foundBook != null)
            {
                Console.WriteLine(foundBook);
            }
            else
            {
                Console.WriteLine("Книга с ISBN не наайдена.");
            }
        }

        public static void PrintBook(int index)
        {
            if (index >= 0 && index < Books.Count)
            {
                Console.WriteLine(Books[index]);
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
        }

        public static void PrintBooks()
        {
            if (Books.Count == 0)
            {
                Console.WriteLine("Библиотека пустая.");
                return;
            }

            Console.WriteLine("Лист книг:");
            for (int i = 0; i < Books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Books[i].Title} - {Books[i].Author} (ISBN: {Books[i].ISBN})");
            }
        }

        public static void ReadFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonString = reader.ReadToEnd();
                    Books = JsonSerializer.Deserialize<List<Book>>(jsonString);
                    Console.WriteLine("Книги загружены в файл.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден. Начинаем с пустой библиотеки");
                Books = new List<Book>(); // Initialize Books here
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        public static void WriteFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    string jsonString = JsonSerializer.Serialize(Books);
                    writer.Write(jsonString);
                    Console.WriteLine("Книги сохранены в файл.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }
    }
}
