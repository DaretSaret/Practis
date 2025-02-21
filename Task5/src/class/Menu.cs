using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Task5
{
    public class Menu
    {
        private Dictionary<string, DeviceData> _configuration;
        private List<string> _csvFiles;
        private DataTable _interpretedData;

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Прочитать конфигурацию");
                Console.WriteLine("2. Вывести конфигурацию");
                Console.WriteLine("3. Прочитать файл с данными");
                Console.WriteLine("4. Вывести строки данных (с N до M)");
                Console.WriteLine("0. Выход");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ReadConfiguration();
                        break;
                    case "2":
                        PrintConfiguration();
                        break;
                    case "3":
                        ReadCsvFiles();
                        break;
                    case "4":
                        PrintDataLines();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private void ReadConfiguration()
        {
            var projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            var configFilePath = Path.Combine(projectDirectory, "configuration", "configuration.json");

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine($"Файл конфигурации отсутствует: {configFilePath}");
                return;
            }

            using (var streamReader = new StreamReader(configFilePath))
            {
                var str = streamReader.ReadToEnd();
                _configuration = JsonSerializer.Deserialize<Dictionary<string, DeviceData>>(str);
                Console.WriteLine($"Файл конфигурации успешно загружен: {configFilePath}");
            }
        }

        private void PrintConfiguration()
        {
            if (_configuration == null || _configuration.Count == 0)
            {
                Console.WriteLine("Отсутствуют данные конфигурации.");
                return;
            }

            foreach (var device in _configuration)
            {
                Console.WriteLine($"ID: {device.Key}, Name: {device.Value.Name}");
            }
        }

        private void ReadCsvFiles()
        {
            var dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csvfiles");

            if (!Directory.Exists(dataDirectory))
            {
                Console.WriteLine($"Папка {dataDirectory} не найдена. Убедитесь, что она существует.");
                return;
            }

            _csvFiles = Directory.GetFiles(dataDirectory, "*.csv").ToList();
            for (int i = 0; i < _csvFiles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_csvFiles[i]}");
            }

            Console.WriteLine("Выберите файл для чтения (введите номер):");
            var fileIndex = int.Parse(Console.ReadLine()) - 1;
            if (fileIndex >= 0 && fileIndex < _csvFiles.Count)
            {
                var csvFilePath = _csvFiles[fileIndex];
                _interpretedData = ReadCsvData(csvFilePath);
                Console.WriteLine($"Данные успешно прочитаны из файла: {csvFilePath}");
            }
            else
            {
                Console.WriteLine("Неверный номер файла.");
            }
        }

        private DataTable ReadCsvData(string filePath)
        {
            var dataTable = new DataTable();

            using (var reader = new StreamReader(filePath))
            {
                var headerLine = reader.ReadLine();
                if (headerLine != null)
                {
                    var headers = headerLine.Split('\t');
                    foreach (var header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split('\t');
                        dataTable.Rows.Add(values);
                    }
                }
            }

            return dataTable;
        }
        private void PrintDataLines()
        {
            if (_interpretedData == null || _interpretedData.Rows.Count == 0)
            {
                Console.WriteLine("Нет данных для отображения.");
                return;
            }

            Console.WriteLine("Введите N и M (через пробел):");
            var input = Console.ReadLine().Split(' ');

            if (input.Length != 2 || !int.TryParse(input[0], out int n) || !int.TryParse(input[1], out int m))
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите два числа.");
                return;
            }

            if (n < 0 || m >= _interpretedData.Rows.Count || n > m)
            {
                Console.WriteLine("Неверный диапазон. Убедитесь, что N и M находятся в пределах существующих строк.");
                return;
            }

            for (int i = n; i <= m; i++)
            {
                var row = _interpretedData.Rows[i];
                Console.WriteLine(string.Join(", ", row.ItemArray));
            }
        }
    }
}
