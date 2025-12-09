namespace Mmc.MonoGame.Noise.Combiners
{
    public class InvertNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        public InvertNoise(INoise sourceNoise)
        {
            SourceNoise = sourceNoise;
        }

        public double GetValue(double x, double y)
        {
            return -SourceNoise.GetValue(x, y);
        }
    }
}
