using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Task10
{
    public partial class MainWindow : Window
    {
        private int currentImageIndex = 0;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            LoadImages();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
        }

        private void LoadImages()
        {
            var images = new List<BitmapImage>
            {
            new BitmapImage(new Uri("pack://application:,,,/img/first.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/img/next.png", UriKind.Absolute)),
            new BitmapImage(new Uri("pack://application:,,,/img/last.png", UriKind.Absolute))
            };
            ImageIndicator.Images = images;
            ImageIndicator.Value = currentImageIndex;
        }

        private void NextImageButton_Click(object sender, RoutedEventArgs e)
        {
            currentImageIndex = (currentImageIndex + 1) % ImageIndicator.Images.Count;
            ImageIndicator.Value = currentImageIndex;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval))
            {
                timer.Interval = TimeSpan.FromMilliseconds(interval);
            }
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentImageIndex = (currentImageIndex + 1) % ImageIndicator.Images.Count;
            ImageIndicator.Value = currentImageIndex;
        }
    }
}