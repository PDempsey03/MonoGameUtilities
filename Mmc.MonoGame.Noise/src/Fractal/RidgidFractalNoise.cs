namespace Mmc.MonoGame.Noise.Fractal
{
    public class RidgidFractalNoise : FractalNoise
    {
        public RidgidFractalNoise(INoise baseNoise, int octaves = 6, double lacunarity = 2d, double gain = 0.5d) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        public RidgidFractalNoise(INoise baseNoise) : base(baseNoise)
        {

        }

        protected override double SummingFunction(double baseNoiseValue, double amplitude, double frequency)
        {
            return Math.Pow(1 - Math.Abs(baseNoiseValue), 2) * amplitude;
        }
    }
}
