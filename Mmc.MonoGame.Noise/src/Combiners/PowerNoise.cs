namespace Mmc.MonoGame.Noise.Combiners
{
    public class PowerNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private double Power { get; init; }

        public PowerNoise(INoise sourceNoise, double power)
        {
            SourceNoise = sourceNoise;
            Power = power;
        }

        public double GetValue(double x, double y)
        {
            return Math.Pow(SourceNoise.GetValue(x, y), Power);
        }
    }
}
