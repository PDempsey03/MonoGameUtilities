namespace Mmc.MonoGame.Noise.Fractal
{
    public class FractalNoise : INoise
    {
        protected INoise BaseNoise { get; init; }

        protected int Octaves { get; init; }

        protected double Lacunarity { get; init; }

        protected double Gain { get; init; }

        public FractalNoise(INoise baseNoise, int octaves = 6, double lacunarity = 2d, double gain = 0.5d)
        {
            BaseNoise = baseNoise;
            Octaves = octaves;
            Lacunarity = lacunarity;
            Gain = gain;
        }

        public virtual double GetValue(double x, double y)
        {
            double sum = 0f;

            double amplitude = 1f;
            double frequency = 1f;

            for (int i = 0; i < Octaves; i++)
            {
                var baseNoiseValue = BaseNoise.GetValue(x * frequency, y * frequency);
                sum += SummingFunction(baseNoiseValue, amplitude, frequency);

                frequency *= Lacunarity;
                amplitude *= Gain;
            }
            return Math.Clamp(sum, -1, 1);
        }

        protected virtual double SummingFunction(double baseNoiseValue, double amplitude, double frequency)
        {
            return baseNoiseValue * amplitude;
        }
    }
}
