using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Threading;
using System.Windows.Media.Media3D;
using VrPlayer.Contracts.Trackers;
using VrPlayer.Helpers;

namespace VrPlayer.Trackers.OSVRTracker
{
    [DataContract]
    unsafe public class OSVRTracker : TrackerBase, ITracker
    {
        [DllImport(@"OSVRWrapper.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int OSVR_Init(string contextname);

        [DllImport(@"OSVRWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int OSVR_GetHMDRotation(float* w, float* x, float* y, float* z);

        [DllImport(@"OSVRWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int OSVR_GetHMDPosition(float* x, float* y, float* z);

        private readonly DispatcherTimer _timer;
            
        public OSVRTracker()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Send);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            _timer.Tick += timer_Tick;
        }

        public override void Load()
        {
            try
            {
                if (!IsEnabled)
                {
                    IsEnabled = true;
                    var result = OSVR_Init("VrPlayer.Trackers.OSVRTracker");
                    ThrowErrorOnResult(result, "Error while initializing OSVR");
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.Error(exc.Message, exc);
                IsEnabled = false;
            }

            _timer.Start();
        }

        public override void Unload()
        {
            _timer.Stop();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                float w, x, y, z;
                var result = OSVR_GetHMDRotation(&w, &x, &y, &z);
                ThrowErrorOnResult(result, "Error while getting rotational data from OSVR");
                RawRotation = new Quaternion(x, -y, z, -w);

                result = OSVR_GetHMDPosition(&x, &y, &z);
                ThrowErrorOnResult(result, "Error while getting positional data from OSVR");
                RawPosition = new Vector3D(x, y, z);

                UpdatePositionAndRotation();
            }
            catch(Exception exc)
            {
                Logger.Instance.Error(exc.Message, exc);
            }
        }

        private static void ThrowErrorOnResult(int result, string message)
        {
            if (result == -1)
            {
                throw new Exception(message);
            }
        }
    }
}