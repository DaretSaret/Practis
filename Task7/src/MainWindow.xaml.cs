using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Task7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetBackgroundColor();
        }

        private void VerifyButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            if (IsValidName(name))
            {
                SecondWindow secondWindow = new SecondWindow(name);
                secondWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid name!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[а-яА-Яa-zA-Z\s]+$");
        }

        private async void SetBackgroundColor()
        {
            string weather = await GetWeatherAsync();
            switch (weather)
            {
                case "Clear":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 0)); 
                    break;
                case "Clouds":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(211, 211, 211));
                    break;
                case "Rain":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255));
                    break;
                case "Snow":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                    break;
                case "Thunderstorm":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(169, 169, 169));
                    break;
                default:
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                    break;
            }
        }

        private async Task<string> GetWeatherAsync()
        {
            string apiKey = "4a9915a2906ad7eb9a52daa1aa560ba2";
            string city = "Novosibirsk"; // типо тут должен быть город
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var weatherData = JObject.Parse(jsonResponse);
                        return weatherData["weather"][0]["main"].ToString();
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка: {response.StatusCode}. Проверьте город и API-ключ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return "Unknown";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Unknown";
            }
        }
    }
}