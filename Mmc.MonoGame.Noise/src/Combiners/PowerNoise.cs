namespace Mmc.MonoGame.Noise.Combiners
{
    public class PowerNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private float Power { get; init; }

        public PowerNoise(INoise sourceNoise, float power)
        {
            SourceNoise = sourceNoise;
            Power = power;
        }

        public float GetValue(float x, float y)
        {
            return MathF.Pow(SourceNoise.GetValue(x, y), Power);
        }
    }
}
