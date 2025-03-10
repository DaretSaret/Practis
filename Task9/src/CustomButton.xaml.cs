using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Task9
{
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnimateButton(0.9, Colors.Gray);
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AnimateButton(1.0, Colors.LightGray);
            RaiseEvent(new RoutedEventArgs(ButtonClickedEvent));
        }

        private void AnimateButton(double scale, Color color)
        {
            var scaleTransform = new ScaleTransform(scale, scale);
            ButtonEllipse.RenderTransform = scaleTransform;

            SolidColorBrush currentBrush = ButtonEllipse.Fill as SolidColorBrush;
            Color currentColor = currentBrush != null ? currentBrush.Color : Colors.LightGray; 

            SolidColorBrush animatedBrush = new SolidColorBrush(currentColor);
            ButtonEllipse.Fill = animatedBrush;

            var colorAnimation = new ColorAnimation(color, TimeSpan.FromMilliseconds(100));
            animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        public static readonly RoutedEvent ButtonClickedEvent = EventManager.RegisterRoutedEvent(
            "ButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomButton));

        public event RoutedEventHandler ButtonClicked
        {
            add { AddHandler(ButtonClickedEvent, value); }
            remove { RemoveHandler(ButtonClickedEvent, value); }
        }
    }
}
