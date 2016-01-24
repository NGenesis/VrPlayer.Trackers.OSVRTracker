using System.Windows.Media.Media3D;

namespace VrPlayer.Contracts.Stabilizers
{
    public interface IStabilizer: ILoadable
    {
        Vector3D Translation { get; set; }
        Quaternion Rotation { get; set; }
        void UpdateCurrentFrame(int frame);
        int GetFramesCount();
    }
}