using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Noise.Fractal
{
    public class FractalNoise : INoise
    {
        protected INoise BaseNoise { get; init; }

        protected int Octaves { get; init; }

        protected float Lacunarity { get; init; }

        protected float Gain { get; init; }

        public FractalNoise(INoise baseNoise, int octaves = 6, float lacunarity = 2f, float gain = 0.5f)
        {
            BaseNoise = baseNoise;
            Octaves = octaves;
            Lacunarity = lacunarity;
            Gain = gain;
        }

        public virtual float GetValue(float x, float y)
        {
            float sum = 0f;

            float amplitude = 1f;
            float frequency = 1f;

            for (int i = 0; i < Octaves; i++)
            {
                var baseNoiseValue = BaseNoise.GetValue(x * frequency, y * frequency);
                sum += SummingFunction(baseNoiseValue, amplitude, frequency);

                frequency *= Lacunarity;
                amplitude *= Gain;
            }
            return MathHelper.Clamp(sum, -1, 1);
        }

        protected virtual float SummingFunction(float baseNoiseValue, float amplitude, float frequency)
        {
            return baseNoiseValue * amplitude;
        }
    }
}
