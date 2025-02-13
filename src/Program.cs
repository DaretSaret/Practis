using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Library.ReadFile();

            while (true)
            {
                Console.WriteLine("\nСистема управления библиотекой");
                Console.WriteLine("1. Чтение из файла");
                Console.WriteLine("2. Сохранение в файл");
                Console.WriteLine("3. Добавить книгу");
                Console.WriteLine("4. Редактировать книгу");
                Console.WriteLine("5. Удалить книгу");
                Console.WriteLine("6. просмотр книги по ISBN");
                Console.WriteLine("7. просмотр всех книг");
                Console.WriteLine("8. Выход");
                Console.Write("Введите ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Library.ReadFile();
                        break;
                    case "2":
                        Library.WriteFile();
                        break;
                    case "3":
                        Library.AddBook();
                        break;
                    case "4":
                        Library.UpdateBook();
                        break;
                    case "5":
                        Library.RemoveBook();
                        break;
                    case "6":
                        Console.WriteLine("Введите ISBN для поиска:");
                        string isbnToSearch = Console.ReadLine();
                        Library.PrintBook(isbnToSearch);
                        break;
                    case "7":
                        Library.PrintBooks();
                        break;
                    case "8":
                        Library.WriteFile();
                        Console.WriteLine("Выход");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
