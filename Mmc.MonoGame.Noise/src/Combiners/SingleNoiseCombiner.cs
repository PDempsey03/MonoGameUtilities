namespace Mmc.MonoGame.Noise.Combiners
{
    public class SingleNoiseCombiner : INoise
    {
        private INoise SourceNoise { get; init; }

        private Func<INoise, float> NoiseFunc { get; init; }

        public SingleNoiseCombiner(INoise sourceNoise, Func<INoise, float> noiseFunc)
        {
            SourceNoise = sourceNoise;
            NoiseFunc = noiseFunc;
        }

        public float GetValue(float x, float y)
        {
            return NoiseFunc?.Invoke(SourceNoise) ?? SourceNoise.GetValue(x, y);
        }
    }
}
