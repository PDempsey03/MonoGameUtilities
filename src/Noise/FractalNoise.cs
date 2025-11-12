namespace Mmc.MonoGame.Utils.Noise
{
    public class FractalNoise : INoise
    {
        private INoise BaseNoise { get; init; }
        private int Octaves { get; init; }
        private float Lacunarity { get; init; }
        private float Gain { get; init; }

        public FractalNoise(INoise baseNoise, int octaves = 6, float lacunarity = 2f, float gain = 0.5f)
        {
            BaseNoise = baseNoise;
            Octaves = octaves;
            Lacunarity = lacunarity;
            Gain = gain;
        }

        public float GetValue(float x, float y)
        {
            float sum = 0f;

            float amplitude = 1f;
            float frequency = 1f;

            for (int i = 0; i < Octaves; i++)
            {
                sum += SummingFunction(BaseNoise.GetValue(x * frequency, y * frequency), amplitude, frequency);

                frequency *= Lacunarity;
                amplitude *= Gain;
            }

            return sum;
        }

        protected virtual float SummingFunction(float baseNoiseValue, float amplitude, float frequency)
        {
            return baseNoiseValue * amplitude;
        }

        // TODO: make these there own sub classes if it works out
        protected virtual float SummingFunctionTurbulence(float baseNoiseValue, float amplitude, float frequency)
        {
            return MathF.Abs(baseNoiseValue) * amplitude;
        }

        protected virtual float SummingFunctionRidgid(float baseNoiseValue, float amplitude, float frequency)
        {
            return MathF.Pow(1 - MathF.Abs(baseNoiseValue), 2) * amplitude;
        }
    }
}
