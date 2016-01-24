using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace VrPlayer.Contracts.Medias
{
    public interface IMedia : ILoadable
    {
        FrameworkElement Media { get; }
        string FileName { get; }

        ICommand OpenFileCommand { get; }
        ICommand OpenDiscCommand { get; }
        ICommand OpenStreamCommand { get; }
        ICommand OpenDeviceCommand { get; }
        ICommand OpenProcessCommand { get; }

        ICommand PlayCommand { get; }
        ICommand PauseCommand { get; }
        ICommand StopCommand { get; }
        ICommand PreviousCommand { get; }
        ICommand NextCommand { get; }
        ICommand SeekCommand { get; }
        ICommand LoopCommand { get; }

        TimeSpan Position { get; set; }
        TimeSpan Duration { get; set; }
        bool IsPlaying { get; set; }
        bool HasDuration { get; }
        bool HasChapters { get; }
        double Progress { get; }
        
        //Todo: Extract to audio plugin
        Vector3D AudioPosition { get; set; }
        Quaternion AudioRotation { get; set; }
    }
}
