namespace Mmc.MonoGame.Noise.Combiners
{
    public class MaxNoise : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        public MaxNoise(INoise noiseA, INoise noiseB)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
        }

        public double GetValue(double x, double y)
        {
            return Math.Max(NoiseA.GetValue(x, y), NoiseB.GetValue(x, y));
        }
    }
}
