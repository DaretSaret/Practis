using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Task7
{
    public partial class SecondWindow : Window
    {
        private const string FilePath = "history.xml";

        public SecondWindow(string name)
        {
            InitializeComponent();
            GreetingLabel.Content = $"Привет, {name}!";
            UpdateHistory(name);
        }

        private void UpdateHistory(string name)
        {
            List<NameEntry> history = LoadHistory();
            history.Add(new NameEntry { Name = name, Time = DateTime.Now });
            SaveHistory(history);
            HistoryDataGrid.ItemsSource = history;
        }

        private List<NameEntry> LoadHistory()
        {
            if (File.Exists(FilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<NameEntry>));
                using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                {
                    return (List<NameEntry>)serializer.Deserialize(stream);
                }
            }
            return new List<NameEntry>();
        }

        private void SaveHistory(List<NameEntry> history)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<NameEntry>));
            using (FileStream stream = new FileStream(FilePath, FileMode.Create))
            {
                serializer.Serialize(stream, history);
            }
        }
        public class NameEntry
        {
            public string Name { get; set; }
            public DateTime Time { get; set; }
        }
    }
}
