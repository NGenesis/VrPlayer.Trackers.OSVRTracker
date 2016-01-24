using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace VrPlayer.Helpers.Converters
{
    public class CompareAllParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!values.Any())
                return false;

            if (parameter != null && parameter.ToString().ToLower() == "string")
            {
                return values.All(val => val.ToString() == values[0].ToString());
            }

            return values.All(val => val == values[0]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}