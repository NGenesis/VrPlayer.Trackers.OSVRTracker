using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Data;

namespace VrPlayer.Helpers.Converters
{
    public class TranslationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var key = value.ToString();
            //Todo: Use resx file from main assembly.
            var resourceManager = new ResourceManager("VrPlayer.Helpers.Properties.Resources", Assembly.GetExecutingAssembly());
            return resourceManager.GetString(key);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}