namespace Mmc.MonoGame.Noise.Fractal
{
    public class TurbulentFractalNoise : FractalNoise
    {
        public TurbulentFractalNoise(INoise baseNoise, int octaves = 6, float lacunarity = 2f, float gain = 0.5f) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        public TurbulentFractalNoise(INoise baseNoise) : base(baseNoise)
        {

        }

        protected override float SummingFunction(float baseNoiseValue, float amplitude, float frequency)
        {
            return MathF.Abs(baseNoiseValue) * amplitude;
        }
    }
}
