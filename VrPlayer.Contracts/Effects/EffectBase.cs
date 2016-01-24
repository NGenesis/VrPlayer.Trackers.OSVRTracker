using System.Windows.Media.Effects;

namespace VrPlayer.Contracts.Effects
{
    public class EffectBase : ShaderEffect, ILoadable
    {
        public void Load() { }
        public void Unload() { }
    }
}