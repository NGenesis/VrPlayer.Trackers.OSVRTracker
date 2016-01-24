using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VrPlayer.Helpers.Converters
{
    public class InvertBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Visible;
            try
            {
                var boolValue = bool.Parse(value.ToString());
                visibility = boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (Exception)
            {
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}