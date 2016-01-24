using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace VrPlayer.Helpers.Converters
{
    public class ObjectToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = (Key)value;
            return key.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Key)Enum.Parse(typeof(Key), value.ToString());
        }
    }
}