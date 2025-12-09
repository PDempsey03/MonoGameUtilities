namespace Mmc.MonoGame.Noise.Combiners
{
    public class SingleNoiseCombiner : INoise
    {
        private INoise SourceNoise { get; init; }

        private Func<INoise, double> NoiseFunc { get; init; }

        public SingleNoiseCombiner(INoise sourceNoise, Func<INoise, double> noiseFunc)
        {
            SourceNoise = sourceNoise;
            NoiseFunc = noiseFunc;
        }

        public double GetValue(double x, double y)
        {
            return NoiseFunc?.Invoke(SourceNoise) ?? SourceNoise.GetValue(x, y);
        }
    }
}
