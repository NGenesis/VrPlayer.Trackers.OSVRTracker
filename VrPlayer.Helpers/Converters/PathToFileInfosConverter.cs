using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;

namespace VrPlayer.Helpers.Converters
{
    [ValueConversion(typeof(string), typeof(IEnumerable<FileInfo>))]
    public class PathToFileInfosConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var path = value.ToString();
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), path);
                }
                var dir = new DirectoryInfo(path);
                var filter = FileFilterHelper.GetFilter();
                return dir.GetFiles().Where(info => filter.Contains(info.Extension));
            }
            catch (Exception exc)
            {
                Logger.Instance.Error("Error while converting path to FileInfos.", exc);
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}