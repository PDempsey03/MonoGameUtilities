namespace Mmc.MonoGame.Utils.Noise.Fractal
{
    public class RidgidFractalNoise : FractalNoise
    {
        public RidgidFractalNoise(INoise baseNoise, int octaves = 6, float lacunarity = 2f, float gain = 0.5f) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        protected override float SummingFunction(float baseNoiseValue, float amplitude, float frequency)
        {
            return MathF.Pow(1 - MathF.Abs(baseNoiseValue), 2) * amplitude;
        }
    }
}
