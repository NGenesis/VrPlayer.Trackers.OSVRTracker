using System;
using System.Windows;
using System.Windows.Input;

namespace VrPlayer.Helpers
{
    public class FullScreenWindow: Window
    {
        private bool _inStateChange;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                WindowState = WindowState.Normal;
            }
            base.OnKeyDown(e);
        }

        public void ToggleFullScreen()
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Maximized && !_inStateChange)
            {
                _inStateChange = true;
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize;
                _inStateChange = false;
            }
            else if (WindowState == WindowState.Normal && !_inStateChange)
            {
                _inStateChange = true;
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResize;
                _inStateChange = false;
            }
            base.OnStateChanged(e);
        }
    }
}
