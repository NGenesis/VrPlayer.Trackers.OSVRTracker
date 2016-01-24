using System;
using System.ComponentModel.Composition;
using VrPlayer.Contracts;
using VrPlayer.Contracts.Trackers;
using VrPlayer.Helpers;

namespace VrPlayer.Trackers.OSVRTracker
{
    [Export(typeof(IPlugin<ITracker>))]
    public class OSVRPlugin : PluginBase<ITracker>
    {
        public OSVRPlugin()
        {
            try
            {
                Name = "OSVR";
                Content = new OSVRTracker();
                Panel = null;
            }
            catch (Exception exc)
            {
                Logger.Instance.Error(string.Format("Error while loading '{0}'", GetType().FullName), exc);
            }
        }
    }
}
