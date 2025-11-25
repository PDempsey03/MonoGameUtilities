namespace Mmc.MonoGame.Utils.Noise.Fractal
{
    public class BillowFractalNoise : FractalNoise
    {
        public BillowFractalNoise(INoise baseNoise, int octaves = 6, float lacunarity = 2f, float gain = 0.5f) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        public BillowFractalNoise(INoise baseNoise) : base(baseNoise)
        {

        }

        protected override float SummingFunction(float baseNoiseValue, float amplitude, float frequency)
        {
            return (2 * MathF.Abs(baseNoiseValue) - 1) * amplitude;
        }
    }
}
