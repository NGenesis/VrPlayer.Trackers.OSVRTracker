using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace VrPlayer.Helpers.Converters
{
    public class QuaternionToCoordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var quaternion = (Quaternion)value;
            var angles = QuaternionHelper.QuaternionToEulerAnglesInDeg(quaternion);

            var axis = parameter.ToString().ToLower();
            switch (axis)
            {
                case "x":
                    return angles.X;
                case "y":
                    return angles.Y;
                case "z":
                    return angles.Z;
                default:
                    throw new Exception("Invalid parameter in QuaternionToCoordConverter.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var coord = (double)value;

            var axis = parameter.ToString().ToLower();
            switch (axis)
            {
                case "x":
                    return QuaternionHelper.EulerAnglesInDegToQuaternion(coord, 0, 0);
                case "y":
                    return QuaternionHelper.EulerAnglesInDegToQuaternion(0, coord, 0);
                case "z":
                    return QuaternionHelper.EulerAnglesInDegToQuaternion(0, 0, coord);
                default:
                    throw new Exception("Invalid parameter in QuaternionToCoordConverter.");
            }
        }
    }
}