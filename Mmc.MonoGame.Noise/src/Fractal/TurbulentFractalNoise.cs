namespace Mmc.MonoGame.Noise.Fractal
{
    public class TurbulentFractalNoise : FractalNoise
    {
        public TurbulentFractalNoise(INoise baseNoise, int octaves = 6, double lacunarity = 2d, double gain = 0.5d) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        public TurbulentFractalNoise(INoise baseNoise) : base(baseNoise)
        {

        }

        protected override double SummingFunction(double baseNoiseValue, double amplitude, double frequency)
        {
            return Math.Abs(baseNoiseValue) * amplitude;
        }
    }
}
