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
        private DataTable _rawData;
        private DataTable _interpretedData;

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("Меню:");

                Console.WriteLine("1. Прочитать конфигурацию");

                if (_configuration != null && _configuration.Count > 0)
                {
                    Console.WriteLine("2. Вывести конфигурацию");
                }

                Console.WriteLine("3. Прочитать файл с данными");

                if (_csvFiles != null && _csvFiles.Count > 0)
                {
                    Console.WriteLine("4. Вывести строки данных (с N до M)");
                }

                if (_rawData != null && _rawData.Rows.Count > 0)
                {
                    Console.WriteLine("5. Интерпретировать данные");
                    Console.WriteLine("6. Вывести полезные данные");
                    Console.WriteLine("7. Экспортировать данные в Excel");
                }

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
                    case "5":
                        InterpretData();
                        break;
                    case "6":
                        PrintInterpretedData();
                        break;
                    case "7":
                        ExportToExcel();
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
                Console.WriteLine($"Загружено устройств: {_configuration.Count}");
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
                _rawData = ReadCsvData(csvFilePath);
                Console.WriteLine($"Данные успешно прочитаны из файла: {csvFilePath}");
                Console.WriteLine($"Прочитано строк: {_rawData.Rows.Count}");
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
                    var headers = headerLine.Split(';');
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
                        var values = line.Split(';');
                        dataTable.Rows.Add(values);
                    }
                }
            }

            return dataTable;
        }

        private void PrintDataLines()
        {
            if (_rawData == null || _rawData.Rows.Count == 0)
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

            if (n < 0 || m >= _rawData.Rows.Count || n > m)
            {
                Console.WriteLine("Неверный диапазон. Убедитесь, что N и M находятся в пределах существующих строк.");
                return;
            }

            for (int i = n; i <= m; i++)
            {
                var row = _rawData.Rows[i];
                Console.WriteLine(string.Join(", ", row.ItemArray));
            }
        }

        private void InterpretData()
        {
            if (_rawData == null || _rawData.Rows.Count == 0)
            {
                Console.WriteLine("Нет данных для интерпретации.");
                return;
            }

            if (_configuration == null || _configuration.Count == 0)
            {
                Console.WriteLine("Конфигурация не загружена.");
                return;
            }

            var interpreter = new DataInterpreter(_configuration);
            _interpretedData = interpreter.InterpretData(_rawData);
            Console.WriteLine("Данные успешно интерпретированы.");
            Console.WriteLine($"Интерпретировано строк: {_interpretedData.Rows.Count}");
        }
        private void PrintInterpretedData()
        {
            if (_interpretedData == null || _interpretedData.Rows.Count == 0)
            {
                Console.WriteLine("Нет интерпретированных данных для отображения.");
                return;
            }

            foreach (DataRow row in _interpretedData.Rows)
            {
                Console.WriteLine(string.Join(", ", row.ItemArray));
            }
            Console.WriteLine("Интерпретированные данные:");
            foreach (DataRow row in _interpretedData.Rows)
            {
                string time = row["Time"].ToString();
                string deviceId = row["DeviceID"].ToString();
                string timeValue = row["Время"].ToString();

                Console.WriteLine($"Time: {time}, DeviceID: {deviceId}, Время: {timeValue}");
            }
        }
        private void ExportToExcel()
        {
            if (_interpretedData == null || _interpretedData.Rows.Count == 0)
            {
                Console.WriteLine("Нет интерпретированных данных для экспорта.");
                return;
            }

            var projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            var exportDirectory = Path.Combine(projectDirectory, "ExcelExport");

            if (!Directory.Exists(exportDirectory))
            {
                Directory.CreateDirectory(exportDirectory);
            }

            Console.WriteLine("Введите название файла (без расширения):");
            var fileName = Console.ReadLine();
            var filePath = Path.Combine(exportDirectory, $"{fileName}.xlsx");

            var exporter = new ExcelExporter();
            exporter.ExportDataTableToExcel(_interpretedData, filePath);
            Console.WriteLine($"Данные успешно экспортированы в файл: {filePath}");
        }
    }
}
