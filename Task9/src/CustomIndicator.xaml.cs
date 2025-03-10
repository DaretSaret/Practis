using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task9
{
    public partial class CustomIndicator : UserControl
    {
        public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register(
            "IsOn", typeof(bool), typeof(CustomIndicator), new PropertyMetadata(false, OnIsOnChanged));

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        public CustomIndicator()
        {
            InitializeComponent();
            UpdateIndicator();
        }

        private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomIndicator;
            control.UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            IndicatorEllipse.Fill = IsOn ? Brushes.Green : Brushes.Red;
        }
    }
}
