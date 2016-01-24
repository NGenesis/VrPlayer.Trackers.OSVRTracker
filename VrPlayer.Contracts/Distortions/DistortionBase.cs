using System.Windows.Media.Effects;

namespace VrPlayer.Contracts.Distortions
{
    public class DistortionBase: ShaderEffect, ILoadable
    {
        public void Load() { }
        public void Unload() { }
    }
}
