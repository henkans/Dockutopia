using System;
using System.Globalization;
using System.Windows.Data;

namespace Dockutopia.Converter
{
    public class RemoveCommandParamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value is string)
            {
                return $"rm {value}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
