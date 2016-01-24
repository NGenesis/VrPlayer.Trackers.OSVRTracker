using System;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace VrPlayer.Contracts.Trackers
{
    public interface ITracker: ILoadable
    {
        bool IsActive { get; set; }
        bool IsEnabled { get; set; }
        Quaternion BaseRotation { get; set; }
        Quaternion Rotation { get; set; }
        Quaternion RotationOffset { get; set; }
        Vector3D BasePosition { get; set; }
        Vector3D Position { get; set; }
        Vector3D PositionOffset { get; set; }
        double PositionScaleFactor { get; set; }
        void Calibrate();

        ICommand MoveForwardCommand { get; }
        ICommand MoveBackwardCommand { get; }
        ICommand MoveLeftCommand { get; }
        ICommand MoveRightCommand { get; }
        ICommand MoveUpCommand { get; }
        ICommand MoveDownCommand { get; }
        ICommand CalibrateCommand { get; }
        ICommand ResetCommand { get; }
    }
}
