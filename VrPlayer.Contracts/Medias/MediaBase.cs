using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using VrPlayer.Helpers.Mvvm;

namespace VrPlayer.Contracts.Medias
{
    public abstract class MediaBase: ViewModelBase, IMedia
    {
        public abstract FrameworkElement Media { get; }
        public string FileName { get; protected set; }

        public ICommand OpenFileCommand { get; protected set; }
        public ICommand OpenDiscCommand { get; protected set; }
        public ICommand OpenStreamCommand { get; protected set; }
        public ICommand OpenDeviceCommand { get; protected set; }
        public ICommand OpenProcessCommand { get; protected set; }
        public ICommand PlayCommand { get; protected set; }
        public ICommand PauseCommand { get; protected set; }
        public ICommand StopCommand { get; protected set; }
        public ICommand PreviousCommand { get; protected set; }
        public ICommand NextCommand { get; protected set; }
        public ICommand SeekCommand { get; protected set; }
        public ICommand LoopCommand { get; protected set; }
        
        private bool _isPlaying;
        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                _isPlaying = value;
                OnPropertyChanged("IsPlaying");
                OnPropertyChanged("Media");
            }
        }

        private TimeSpan _position;
        public TimeSpan Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
                OnPropertyChanged("Progress");
                OnPropertyChanged("Media");
            }
        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged("Duration");
                OnPropertyChanged("HasDuration");
                OnPropertyChanged("Media");
            }
        }

        public bool HasDuration
        {
            get
            {
                return Duration.TotalMilliseconds > 0;
            }
        }

        private bool _hasChapters;
        public bool HasChapters
        {
            get
            {
                //Todo: Reactivate when dvd chapters will behave
                return false;// _hasChapters;
            }
            set
            {
                _hasChapters = value;
                OnPropertyChanged("HasChapters");
            }
        }

        public double Progress
        {
            get
            {
                return (Position.TotalMilliseconds / Duration.TotalMilliseconds) * 100;
            }
        }

        protected bool CanPlay(object o)
        {
            return HasDuration && !IsPlaying;
        }

        protected bool CanPause(object o)
        {
            return IsPlaying;
        }

        protected bool CanStop(object o)
        {
            return true;
        }

        protected bool CanSeek(object o)
        {
            return HasDuration;
        }

        protected void Reset()
        {
            IsPlaying = false;
            Position = TimeSpan.Zero;
            Duration = TimeSpan.Zero; 
        }

        public abstract void Load();
        public abstract void Unload();

        public Vector3D AudioPosition { get; set; }
        public Quaternion AudioRotation { get; set; }
    }
}
