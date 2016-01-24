//Source: http://stackoverflow.com/questions/1316405/how-to-set-a-top-margin-only-in-xaml
using System;
using System.Windows;
using System.Windows.Data;

namespace VrPlayer.Helpers.Converters
{
    public class ThicknessMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var factor = System.Convert.ToDouble(parameter);
            var horizontal = System.Convert.ToDouble(values[0]) * factor;
            var vertical = System.Convert.ToDouble(values[1]) * factor;
            return new Thickness(horizontal, vertical, -horizontal, -vertical);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}