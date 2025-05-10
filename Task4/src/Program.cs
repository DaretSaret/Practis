using System;
using System.Collections.Generic;
using System.IO;

namespace Task4
{
    public class Program
    {
        private static List<Worker> workers = new List<Worker>();
        private const string Filename = "workers.csv";
        private const string LogFile = "error_log.txt";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить работника");
                Console.WriteLine("2. Редактировать работника");
                Console.WriteLine("3. Удалить работника");
                Console.WriteLine("4. Вывести на экран");
                Console.WriteLine("5. Сохранить в файл");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("0. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddWorker();
                        break;
                    case "2":
                        EditWorker();
                        break;
                    case "3":
                        RemoveWorker();
                        break;
                    case "4":
                        DisplayWorkers();
                        break;
                    case "5":
                        SaveWorkersToFile();
                        break;
                    case "6":
                        LoadWorkersFromFile();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private static void AddWorker()
        {
            try
            {
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                Console.Write("Введите возраст: ");
                if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
                {
                    throw new ArgumentException("Возраст должен быть положительным числом.");
                }
                Console.Write("Введите дату приёма на работу (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfEmployment))
                {
                    throw new FormatException("Неверный формат даты.");
                }
                Console.Write("Введите категорию: ");
                string grade = Console.ReadLine();

                workers.Add(new Worker(name, age, dateOfEmployment, grade));
                Console.WriteLine("Работник добавлен.");
            }
            catch (FormatException ex)
            {
                LogError(ex);
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                LogError(ex);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private static void EditWorker()
        {
            DisplayWorkers();
            Console.Write("Введите индекс работника для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < workers.Count)
            {
                var worker = workers[index];
                Console.Write($"Редактировать имя (текущее: {worker.Name}): ");
                worker.Name = Console.ReadLine();
                Console.Write($"Редактировать возраст (текущий: {worker.Age}): ");
                if (int.TryParse(Console.ReadLine(), out int age) && age >= 0)
                {
                    worker.Age = age;
                }
                Console.Write($"Редактировать дату (текущая: {worker.DateOfEmployment:yyyy-MM-dd}): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    worker.DateOfEmployment = date;
                }
                Console.Write($"Редактировать категорию (текущая: {worker.Grade}): ");
                worker.Grade = Console.ReadLine();

                Console.WriteLine("Работник обновлен.");
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }
        }

        private static void RemoveWorker()
        {
            DisplayWorkers();
            Console.Write("Введите индекс работника для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < workers.Count)
            {
                workers.RemoveAt(index);
                Console.WriteLine("Работник удален.");
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }
        }

        private static void DisplayWorkers()
        {
            if (workers.Count == 0)
            {
                Console.WriteLine("Нет работников для отображения.");
                return;
            }

            for (int i = 0; i < workers.Count; i++)
            {
                Console.WriteLine($"{i}: {workers[i]}");
            }
        }

        private static void SaveWorkersToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename))
                {
                    writer.WriteLine("Name;Age;DateOfEmployment;Grade");
                    foreach (var worker in workers)
                    {
                        writer.WriteLine(worker);
                    }
                }
                Console.WriteLine("Работники сохранены в файл.");
            }
            catch (FileNotFoundException ex)
            {
                LogError(ex);
                Console.WriteLine("Файл не найден: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                LogError(ex);
                Console.WriteLine("Доступ к файлу запрещен: " + ex.Message);
            }
            catch (IOException ex)
            {
                LogError(ex);
                Console.WriteLine("Ошибка ввода-вывода: " + ex.Message);
            }
        }

        private static void LoadWorkersFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Filename))
                {
                    // Пропуск заголовков
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var parts = line.Split(';');
                        var worker = new Worker(parts[0], int.Parse(parts[1]), DateTime.Parse(parts[2]), parts[3]);
                        workers.Add(worker);
                    }
                }
                Console.WriteLine("Работники загружены из файла.");
            }
            catch (FileNotFoundException ex)
            {
                LogError(ex);
                Console.WriteLine("Файл не найден: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                LogError(ex);
                Console.WriteLine("Доступ к файлу запрещен: " + ex.Message);
            }
            catch (IOException ex)
            {
                LogError(ex);
                Console.WriteLine("Ошибка ввода-вывода: " + ex.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private static void LogError(Exception ex)
        {
            using (StreamWriter logWriter = new StreamWriter(LogFile, true))
            {
                logWriter.WriteLine($"{DateTime.Now}: {ex.Message}");
            }
        }
    }

}

