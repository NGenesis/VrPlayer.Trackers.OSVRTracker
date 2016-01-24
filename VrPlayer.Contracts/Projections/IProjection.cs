using System.Windows.Media.Media3D;

namespace VrPlayer.Contracts.Projections
{
    public interface IProjection: ILoadable
    {
        MeshGeometry3D Geometry { get; }
        Vector3D CameraLeftPosition { get; }
        Vector3D CameraRightPosition { get; }
        StereoMode StereoMode { get; set; }
    }
}
