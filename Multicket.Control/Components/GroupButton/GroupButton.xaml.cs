using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Multicket.Module.Components
{
    /// <summary>
    /// Interaction logic for GroupButton.xaml
    /// </summary>
    public class GroupButton : RadioButton
    {

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(GroupButton), new PropertyMetadata(null));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(GroupButton), new PropertyMetadata(Double.NaN));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(GroupButton), new PropertyMetadata(Double.NaN));



        public string KeyContent
        {
            get { return (string)GetValue(KeyContentProperty); }
            set { SetValue(KeyContentProperty, value); }
        }

        public static readonly DependencyProperty KeyContentProperty =
            DependencyProperty.Register("KeyContent", typeof(string), typeof(GroupButton), new PropertyMetadata(""));



        public FontFamily FontFamilyKeyContent
        {
            get { return (FontFamily)GetValue(FontFamilyKeyContentProperty); }
            set { SetValue(FontFamilyKeyContentProperty, value); }
        }

        public static readonly DependencyProperty FontFamilyKeyContentProperty =
            DependencyProperty.Register("FontFamilyKeyContent", typeof(FontFamily), typeof(GroupButton), new PropertyMetadata(null));



        public FontWeight FontWeightKeyContent
        {
            get { return (FontWeight)GetValue(FontWeightKeyContentProperty); }
            set { SetValue(FontWeightKeyContentProperty, value); }
        }

        public static readonly DependencyProperty FontWeightKeyContentProperty =
            DependencyProperty.Register("FontWeightKeyContent", typeof(FontWeight), typeof(GroupButton), new PropertyMetadata(FontWeights.Normal));




        static GroupButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupButton), new FrameworkPropertyMetadata(typeof(GroupButton)));
        }
    }
}
