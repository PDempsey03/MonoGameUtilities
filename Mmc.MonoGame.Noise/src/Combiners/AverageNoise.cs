namespace Mmc.MonoGame.Noise.Combiners
{
    public class AverageNoise : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        public AverageNoise(INoise noiseA, INoise noiseB)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
        }

        public float GetValue(float x, float y)
        {
            return (NoiseA.GetValue(x, y) + NoiseB.GetValue(x, y)) / 2;
        }
    }
}
