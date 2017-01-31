using System;
using System.Globalization;
using System.Windows.Data;

namespace Dockutopia.Converter
{

    public class IpCommandParamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null) return null;
            var command = @"inspect --format {{.NetworkSettings.Networks.nat.IPAddress}} ";
            if (value is string)
            {
                return command + value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
