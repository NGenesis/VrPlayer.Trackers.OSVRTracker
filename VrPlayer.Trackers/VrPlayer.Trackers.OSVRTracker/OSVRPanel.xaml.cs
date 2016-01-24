using System;
using System.Windows.Controls;

namespace VrPlayer.Trackers.OSVRTracker
{
    public partial class OSVRPanel : UserControl
    {
        public OSVRPanel(OSVRTracker tracker)
        {
            InitializeComponent();
            try
            {
                DataContext = tracker;
            }
            catch (Exception exc)
            {
            }
        }
    }
}
