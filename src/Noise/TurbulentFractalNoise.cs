namespace Mmc.MonoGame.Utils.Noise
{
    public class TurbulentFractalNoise : FractalNoise
    {
        public TurbulentFractalNoise(INoise baseNoise, int octaves = 6, float lacunarity = 2f, float gain = 0.5f) : base(baseNoise, octaves, lacunarity, gain)
        {

        }

        protected override float SummingFunction(float baseNoiseValue, float amplitude, float frequency)
        {
            return MathF.Abs(baseNoiseValue) * amplitude;
        }
    }
}
