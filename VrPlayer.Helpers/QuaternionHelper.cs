using System;
using System.Windows.Media.Media3D;

namespace VrPlayer.Helpers
{
    public class QuaternionHelper
    {
        private const int Digits = 6;

        public static Quaternion EulerAnglesInDegToQuaternion(Vector3D angles)
        {
            return EulerAnglesInDegToQuaternion(angles.X, angles.Y, angles.Z);
        }

        public static Quaternion EulerAnglesInDegToQuaternion(double pitch, double yaw, double roll)
        {
            return EulerAnglesInRadToQuaternion(
                DegreeToRadian(pitch),
                DegreeToRadian(yaw),
                DegreeToRadian(roll));
        }

        public static Quaternion EulerAnglesInRadToQuaternion(Vector3D angles)
        {
            return EulerAnglesInRadToQuaternion(angles.X, angles.Y, angles.Z);
        }

        //Source: http://www.euclideanspace.com/maths/geometry/rotations/conversions/eulerToQuaternion/index.htm
        public static Quaternion EulerAnglesInRadToQuaternion(double pitch, double yaw, double roll)
        {
            var c1 = Math.Cos(-yaw / 2);
            var s1 = Math.Sin(-yaw / 2);
            var c2 = Math.Cos(roll / 2);
            var s2 = Math.Sin(roll / 2);
            var c3 = Math.Cos(-pitch / 2);
            var s3 = Math.Sin(-pitch / 2);
            var c1c2 = c1 * c2;
            var s1s2 = s1 * s2;
            var q = new Quaternion
                {
                    W = c1c2*c3 - s1s2*s3,
                    X = c1c2*s3 + s1s2*c3,
                    Y = s1*c2*c3 + c1*s2*s3,
                    Z = c1*s2*c3 - s1*c2*s3
                };
            return FormatQuaternion(q);
        }

        public static Vector3D QuaternionToEulerAnglesInDeg(Quaternion q)
        {
            return Format(RadianToDegree(QuaternionToEulerAnglesInRad(q)));
        }

        //Source: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/index.htm
        public static Vector3D QuaternionToEulerAnglesInRad(Quaternion q)
        {
            q = FormatQuaternion(q);

            var sqw = Math.Pow(q.W,2);
            var sqx = Math.Pow(q.X,2);
            var sqy = Math.Pow(q.Y, 2);
            var sqz = Math.Pow(q.Z, 2);
	        var unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
	        var test = q.X*q.Y + q.Z*q.W;
	        if (test > 0.499*unit) 
            { // singularity at north pole
	            return new Vector3D
	                {
	                    Y = 2*Math.Atan2(q.X, q.W),
	                    Z = Math.PI/2,
	                    X = 0
	                };
	        }
	        if (test < -0.499*unit) 
            { // singularity at south pole
	            return new Vector3D
	                {
	                    Y = -2*Math.Atan2(q.X, q.W),
	                    Z = -Math.PI/2,
	                    X = 0
	                };
	        }
            return new Vector3D
                {
                    Y = -Math.Atan2(2*q.Y*q.W - 2*q.X*q.Z, sqx - sqy - sqz + sqw),
                    Z = Math.Asin(2 * test / unit),
                    X = -Math.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z, -sqx + sqy - sqz + sqw)
            };
        }

        #region Formatting

        private static Quaternion FormatQuaternion(Quaternion q)
        {
            return new Quaternion
                {
                    X = Math.Round(q.X, Digits),
                    Y = Math.Round(q.Y, Digits),
                    Z = Math.Round(q.Z, Digits),
                    W = Math.Round(q.W, Digits)
                };
        }

        private static Vector3D Format(Vector3D v)
        {
            return new Vector3D
            {
                X = NormaliseAngle(Math.Round(v.X)),
                Y = NormaliseAngle(Math.Round(v.Y)),
                Z = NormaliseAngle(Math.Round(v.Z))
            };
        }

        //Source: http://stackoverflow.com/questions/3176849/clockwise-angle-between-two-line
        public static double NormaliseAngle(double degrees)
        {
            return ((degrees % 360) + 360) % 360;
        }

        #endregion

        #region Specifics Vectors

        public static Vector3D FrontVectorFromQuaternion(Quaternion q)
        {
            return new Vector3D(
                2 * (q.X * q.Z + q.W * q.Y),
                2 * (q.Y * q.Z - q.W * q.X),
                1 - 2 * (q.X * q.X + q.Y * q.Y)
            );
        }

        public static Vector3D UpVectorFromQuaternion(Quaternion q)
        {
            return new Vector3D(
                2 * (q.X * q.Y - q.W * q.Z),
                1 - 2 * (q.X * q.X + q.Z * q.Z),
                2 * (q.Y * q.Z + q.W * q.X)
            );
        }

        #endregion

        #region Deg / Rad conversion

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static Vector3D DegreeToRadian(Vector3D angles)
        {
            return new Vector3D
            {
                X = DegreeToRadian(angles.X),
                Y = DegreeToRadian(angles.Y),
                Z = DegreeToRadian(angles.Z)
            };
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static Vector3D RadianToDegree(Vector3D angles)
        {
            return new Vector3D
            {
                X = RadianToDegree(angles.X),
                Y = RadianToDegree(angles.Y),
                Z = RadianToDegree(angles.Z)
            };
        }

        #endregion
    }
}