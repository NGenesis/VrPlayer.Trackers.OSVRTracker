using System.ComponentModel;
using System.Windows;

namespace VrPlayer.Contracts
{
    public interface IPlugin<T> : ILoadable, INotifyPropertyChanged
    {
        string Name { get; set; }
        T Content { get; set; }
        FrameworkElement Panel { get; set; }
        PluginConfig ExtractConfig();
        void InjectConfig(PluginConfig plugin);
    }
}