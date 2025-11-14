namespace Mmc.MonoGame.Utils.Noise.Combiners
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

        public float GetValue(float x, float y)
        {
            return MathF.Max(NoiseA.GetValue(x, y), NoiseB.GetValue(x, y));
        }
    }
}
