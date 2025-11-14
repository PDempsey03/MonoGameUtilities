namespace Mmc.MonoGame.Utils.Noise.Combiners
{
    public class DoubleNoiseCombiner : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        private Func<INoise, INoise, float> NoiseFunc { get; init; }

        public DoubleNoiseCombiner(INoise noiseA, INoise noiseB, Func<INoise, INoise, float> noiseFunc)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
            NoiseFunc = noiseFunc;
        }

        public float GetValue(float x, float y)
        {
            return NoiseFunc?.Invoke(NoiseA, NoiseB) ?? NoiseA.GetValue(x, y);
        }
    }
}
