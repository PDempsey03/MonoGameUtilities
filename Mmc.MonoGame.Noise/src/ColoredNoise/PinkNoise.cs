namespace Mmc.MonoGame.Noise.ColoredNoise
{
    public class PinkNoise : INoise
    {
        private WhiteNoise WhiteNoise { get; init; }

        private int Octaves { get; init; }

        public PinkNoise(int seed, int octaves)
        {
            WhiteNoise = new WhiteNoise(seed);
            Octaves = octaves;
        }

        public float GetValue(float x, float y)
        {
            float result = 0;
            float totalWeight = 0;

            float frequency = 1;
            float amplitude = 1;

            for (int i = 0; i < Octaves; i++)
            {
                result += WhiteNoise.GetValue(x * frequency, y * frequency) * amplitude;
                totalWeight += amplitude;

                frequency *= 2;
                amplitude /= 2;
            }

            return result / totalWeight;
        }
    }
}
