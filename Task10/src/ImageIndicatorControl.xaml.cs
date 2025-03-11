using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Task10
{ 
    public partial class ImageIndicatorControl : UserControl
    {
        public static readonly DependencyProperty ImagesProperty =
            DependencyProperty.Register("Images", typeof(List<BitmapImage>), typeof(ImageIndicatorControl), new PropertyMetadata(new List<BitmapImage>(), OnImagesChanged));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ImageIndicatorControl), new PropertyMetadata(0, OnValueChanged));

        public List<BitmapImage> Images
        {
            get { return (List<BitmapImage>)GetValue(ImagesProperty); }
            set { SetValue(ImagesProperty, value); }
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public ImageIndicatorControl()
        {
            InitializeComponent();
        }

        private static void OnImagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageIndicatorControl;
            control.UpdateImage();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageIndicatorControl;
            control.UpdateImage();
        }

        private void UpdateImage()
        {
            if (Images == null || Images.Count == 0)
            {
                DisplayImage.Source = null;
                return;
            }

            if (Value < 0 || Value >= Images.Count)
            {
                DisplayImage.Source = new BitmapImage(new System.Uri("pack://application:,,,/img/placeholder.png"));
            }
            else
            {
                DisplayImage.Source = Images[Value];
            }
        }
    }
}
