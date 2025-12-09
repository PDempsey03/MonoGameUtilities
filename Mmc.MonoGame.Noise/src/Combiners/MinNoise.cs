namespace Mmc.MonoGame.Noise.Combiners
{
    public class MinNoise : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        public MinNoise(INoise noiseA, INoise noiseB)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
        }

        public double GetValue(double x, double y)
        {
            return Math.Min(NoiseA.GetValue(x, y), NoiseB.GetValue(x, y));
        }
    }
}
