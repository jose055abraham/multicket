using System;
using System.Globalization;
using System.Windows.Data;

namespace Multicket.Module
{
    public class NumberFormat : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value >= 0)
            {
                return (int)value;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
