namespace Mmc.MonoGame.Noise.Combiners
{
    public class ClampedNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private double MinValue { get; init; }

        private double MaxValue { get; init; }

        public ClampedNoise(INoise sourceNoise, double minValue, double maxValue)
        {
            SourceNoise = sourceNoise;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public double GetValue(double x, double y)
        {
            return Math.Clamp(SourceNoise.GetValue(x, y), MinValue, MaxValue);
        }
    }
}
