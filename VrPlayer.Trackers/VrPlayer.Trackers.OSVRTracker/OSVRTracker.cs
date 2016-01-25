using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows.Threading;
using System.Windows.Media.Media3D;
using System.Windows.Input;
using VrPlayer.Contracts.Trackers;
using VrPlayer.Helpers;

namespace VrPlayer.Trackers.OSVRTracker
{
    [DataContract]
    unsafe public class OSVRTracker : TrackerBase, ITracker
    {
        [DllImport(@"OSVRWrapper.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool OSVR_Init(string contextname);

        [DllImport(@"OSVRWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void OSVR_Update();

        [DllImport(@"OSVRWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void OSVR_GetHMDRotation(double* w, double* x, double* y, double* z);

        [DllImport(@"OSVRWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void OSVR_GetHMDPosition(double* x, double* y, double* z);

        [DllImport(@"OSVRWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void OSVR_ResetHMDRotationFromHead();

        private readonly DispatcherTimer _timer;
            
        public OSVRTracker()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Send);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            _timer.Tick += timer_Tick;
        }

        public override void Calibrate()
        {
            try
            {
                // Recenter All View Axes When [Shift] + [KeyTrackerCalibrate]
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    RawRotation.Y = 0;
                    RawRotation.Normalize();

                    base.Calibrate();
                }
                
                // Recenter HMD Using Current Head Rotation
                OSVR_ResetHMDRotationFromHead();

            }
            catch (Exception exc)
            {
                Logger.Instance.Error(exc.Message, exc);
            }
        }

        public override void Load()
        {
            try
            {
                if (!IsEnabled)
                {
                    IsEnabled = true;
                    if(!OSVR_Init("VrPlayer.Trackers.OSVRTracker")) throw new Exception("Error while initializing OSVR");
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
                // Update HMD Tracker State
                OSVR_Update();

                // Retrieve New Position & Rotation From HMD
                double w, x, y, z;
                OSVR_GetHMDRotation(&w, &x, &y, &z);
                RawRotation = new Quaternion(x, -y, z, -w);

                OSVR_GetHMDPosition(&x, &y, &z);
                RawPosition = new Vector3D(x, y, z);

                UpdatePositionAndRotation();
            }
            catch(Exception exc)
            {
                Logger.Instance.Error(exc.Message, exc);
            }
        }
    }
}