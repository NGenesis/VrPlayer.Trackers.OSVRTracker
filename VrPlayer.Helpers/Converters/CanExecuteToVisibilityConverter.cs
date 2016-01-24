using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace VrPlayer.Helpers.Converters
{
    public class CanExecuteToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = Visibility.Collapsed;
            try
            {
                var command = (ICommand) value;
                visibility = command.CanExecute(parameter) ? Visibility.Visible : Visibility.Collapsed;
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