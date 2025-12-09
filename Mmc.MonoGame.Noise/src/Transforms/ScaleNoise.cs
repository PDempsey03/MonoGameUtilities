namespace Mmc.MonoGame.Noise.Transforms
{
    public class ScaleNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private double XScale { get; init; }

        private double YScale { get; init; }

        public ScaleNoise(INoise sourceNoise, double xScale, double yScale)
        {
            SourceNoise = sourceNoise;
            XScale = xScale;
            YScale = yScale;
        }

        public double GetValue(double x, double y)
        {
            return SourceNoise.GetValue(x * XScale, y * YScale);
        }
    }
}
