using System.Windows;

namespace Task9
{
    public partial class MainWindow : Window
    {
            public MainWindow()
            {
                InitializeComponent();
            }

            private void Button_ButtonClicked(object sender, RoutedEventArgs e)
            {
                indicator.IsOn = !indicator.IsOn;
            }
        }
}