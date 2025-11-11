namespace Mmc.MonoGame.Utils.Noise
{
    public class BrownNoise
    {
        private WhiteNoise WhiteNoise { get; set; }

        private int Octaves { get; set; }

        public BrownNoise(int seed, int octaves)
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
                amplitude /= 4;
            }

            return result / totalWeight;
        }
    }
}
