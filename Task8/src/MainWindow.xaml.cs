using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Task8
{
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point clickPosition;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void draggableEllipseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickPosition = e.GetPosition((Ellipse)sender);
            Mouse.Capture((Ellipse)sender);
        }

        private void draggableEllipseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.Capture(null);
        }
        private void draggableEllipseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && sender is Ellipse ellipse)
            {
                var canvas = ellipse.Parent as Canvas;
                Point currentPosition = e.GetPosition(canvas);
                Canvas.SetLeft(ellipse, currentPosition.X - clickPosition.X);
                Canvas.SetTop(ellipse, currentPosition.Y - clickPosition.Y);
            }
        }
        private void FontSizeSlider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mainTextBox.FontSize = e.NewValue;
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            mainTextBox.Text = ((Button)sender).Content + " появилась тут";
        }

        private void ComboBox(object sender, SelectionChangedEventArgs e)
        {
            var color = ((ComboBoxItem)colorComboBox.SelectedItem).Content.ToString();
            Background = color switch
            {
                "Красный" => Brushes.Red,
                "Зеленый" => Brushes.Green,
                "Синий" => Brushes.Blue,
                _ => Brushes.White
            };
        }

        private void ListBox(object sender, SelectionChangedEventArgs e)
        {
            if (itemsListBox.SelectedItem != null)
            {
                mainTextBox.Text = ((ListBoxItem)itemsListBox.SelectedItem).Content.ToString();
            }
        }

        private void CheckBoxChecked(object sender, RoutedEventArgs e)
        {
            progressBar.IsEnabled = true;
            progressBar.Visibility = Visibility.Visible;
        }

        private void CheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            progressBar.IsEnabled = false;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void DatePicker(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                mainTextBox.Text = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy");
            }
        }
        private void ClearButton(object sender, RoutedEventArgs e)
        {
            mainTextBox.Clear();
        }
    }
}