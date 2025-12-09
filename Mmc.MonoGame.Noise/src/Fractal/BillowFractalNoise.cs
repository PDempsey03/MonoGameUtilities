namespace Mmc.MonoGame.Noise.Fractal
{
    public class BillowFractalNoise : FractalNoise
    {
        public BillowFractalNoise(INoise baseNoise, int octaves = 6, double lacunarity = 2d, double gain = 0.5d) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        public BillowFractalNoise(INoise baseNoise) : base(baseNoise)
        {

        }

        protected override double SummingFunction(double baseNoiseValue, double amplitude, double frequency)
        {
            return (2 * Math.Abs(baseNoiseValue) - 1) * amplitude;
        }
    }
}
