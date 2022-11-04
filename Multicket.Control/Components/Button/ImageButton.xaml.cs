using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Multicket.Module.Components
{
    public class ImageButton : Button
    {
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(ImageButton), new PropertyMetadata(Double.NaN));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(ImageButton), new PropertyMetadata(Double.NaN));



        public string KeyContent
        {
            get { return (string)GetValue(KeyContentProperty); }
            set { SetValue(KeyContentProperty, value); }
        }

        public static readonly DependencyProperty KeyContentProperty =
            DependencyProperty.Register("KeyContent", typeof(string), typeof(ImageButton), new PropertyMetadata(""));



        public FontFamily FontFamilyKeyContent
        {
            get { return (FontFamily)GetValue(FontFamilyKeyContentProperty); }
            set { SetValue(FontFamilyKeyContentProperty, value); }
        }

        public static readonly DependencyProperty FontFamilyKeyContentProperty =
            DependencyProperty.Register("FontFamilyKeyContent", typeof(FontFamily), typeof(ImageButton), new PropertyMetadata(null));



        public FontWeight FontWeightKeyContent
        {
            get { return (FontWeight)GetValue(FontWeightKeyContentProperty); }
            set { SetValue(FontWeightKeyContentProperty, value); }
        }

        public static readonly DependencyProperty FontWeightKeyContentProperty =
            DependencyProperty.Register("FontWeightKeyContent", typeof(FontWeight), typeof(ImageButton), new PropertyMetadata(FontWeights.Normal));




        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }
    }
}
