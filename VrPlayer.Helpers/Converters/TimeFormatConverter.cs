using System;
using System.Globalization;
using System.Windows.Data;

namespace VrPlayer.Helpers.Converters
{
    [ValueConversion(typeof(long), typeof(string))]
    public class TimeFormatConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var duration = (TimeSpan)value;
            return duration.ToString(@"hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}