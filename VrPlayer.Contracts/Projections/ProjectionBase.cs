using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using VrPlayer.Helpers.Mvvm;

namespace VrPlayer.Contracts.Projections
{
    public abstract class ProjectionBase: ViewModelBase, IProjection
    {
        private StereoMode _stereoMode;
        private readonly Vector3D _cameraLeftPosition;
        private readonly Vector3D _cameraRightPosition;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            OnPropertyChanged("Geometry");
        }

        public StereoMode StereoMode
        {
            get { return _stereoMode; }
            set 
            {
                _stereoMode = value;
                OnPropertyChanged("StereoMode");
                OnPropertyChanged("Geometry");
            }
        }

        public ProjectionBase()
        {
            _stereoMode = StereoMode.Mono;
            var defaultCameraPosition = new Vector3D(0, 0, 0);
            _cameraLeftPosition = defaultCameraPosition;
            _cameraRightPosition = defaultCameraPosition;
        }

        public virtual MeshGeometry3D Geometry 
        {
            get
            {
                var geometry = new MeshGeometry3D();
                geometry.Positions = Positions;
                geometry.TriangleIndices = TriangleIndices;
                switch (_stereoMode)
                {
                    case StereoMode.OverUnder:
                        geometry.TextureCoordinates = OverUnderTextureCoordinates;
                        break;
                    case StereoMode.SideBySide:
                        geometry.TextureCoordinates = SideBySideTextureCoordinates;
                        break;
                    default:
                        geometry.TextureCoordinates = MonoTextureCoordinates;
                        break;
                }
                return geometry;
            }
        }

        public Vector3D CameraLeftPosition
        {
            get { return _cameraLeftPosition; }
        }

        public Vector3D CameraRightPosition
        {
            get { return _cameraRightPosition; }
        }

        public void Load() { }
        public void Unload() { }

        public abstract Point3DCollection Positions { get; }
        public abstract Int32Collection TriangleIndices { get; }
        public abstract PointCollection MonoTextureCoordinates { get; }
        public abstract PointCollection OverUnderTextureCoordinates { get; }
        public abstract PointCollection SideBySideTextureCoordinates { get; }
    }
}
