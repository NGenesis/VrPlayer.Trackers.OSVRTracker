using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace VrPlayer.Helpers
{
    public class ConfigHelper
    {
        public static Configuration LoadConfig()
        {
            var currentAssembly = Assembly.GetCallingAssembly();
            return ConfigurationManager.OpenExeConfiguration(currentAssembly.Location);
        }

        public static double ParseDouble(string value)
        {
            var ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            return double.Parse(value, NumberStyles.Any, ci);
        }

        public static Color ParseColor(string value)
        {
            var color = ColorConverter.ConvertFromString(value);
            if (color == null)
                return new Color();

            return (Color)color;
        }

        public static Vector3D ParseVector3D(string value)
        {
            var coords = value.Split(',');

            if (coords.Length != 3)
                return new Vector3D();

            var pitch = ParseDouble(coords[0]);
            var yaw = ParseDouble(coords[1]);
            var roll = ParseDouble(coords[2]);
            return new Vector3D(pitch, yaw, roll);
        }


        public static Quaternion ParseQuaternion(string value)
        {
            var q = new Quaternion();

            if(string.IsNullOrEmpty(value))
                return q;

            var coords = value.Split(',');
            if (coords.Length == 4)
            {
                q.X = ParseDouble(coords[0]);
                q.Y = ParseDouble(coords[1]);
                q.Z = ParseDouble(coords[2]);
                q.W = ParseDouble(coords[3]);
            }

            return q;
        }
    }
}