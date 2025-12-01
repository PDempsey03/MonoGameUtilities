using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Noise.Combiners
{
    public class ClampedNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private float MinValue { get; init; }

        private float MaxValue { get; init; }

        public ClampedNoise(INoise sourceNoise, float minValue, float maxValue)
        {
            SourceNoise = sourceNoise;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public float GetValue(float x, float y)
        {
            return MathHelper.Clamp(SourceNoise.GetValue(x, y), MinValue, MaxValue);
        }
    }
}
