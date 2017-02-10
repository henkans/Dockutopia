using System;
using System.Globalization;
using System.Windows.Data;

namespace Dockutopia.Converter
{
    public class StringToBoolConverter : IValueConverter
    { 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inputValue = value as string;
            return !string.IsNullOrEmpty(inputValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
