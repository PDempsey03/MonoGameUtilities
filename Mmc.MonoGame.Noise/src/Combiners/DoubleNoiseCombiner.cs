namespace Mmc.MonoGame.Noise.Combiners
{
    public class DoubleNoiseCombiner : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        private Func<INoise, INoise, double> NoiseFunc { get; init; }

        public DoubleNoiseCombiner(INoise noiseA, INoise noiseB, Func<INoise, INoise, double> noiseFunc)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
            NoiseFunc = noiseFunc;
        }

        public double GetValue(double x, double y)
        {
            return NoiseFunc?.Invoke(NoiseA, NoiseB) ?? NoiseA.GetValue(x, y);
        }
    }
}
