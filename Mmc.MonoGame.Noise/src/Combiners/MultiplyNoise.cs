namespace Mmc.MonoGame.Noise.Combiners
{
    public class MultiplyNoise : INoise
    {
        private INoise NoiseA { get; init; }

        private INoise NoiseB { get; init; }

        public MultiplyNoise(INoise noiseA, INoise noiseB)
        {
            NoiseA = noiseA;
            NoiseB = noiseB;
        }

        public float GetValue(float x, float y)
        {
            return NoiseA.GetValue(x, y) * NoiseB.GetValue(x, y);
        }
    }
}
