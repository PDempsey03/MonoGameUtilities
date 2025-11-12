namespace Mmc.MonoGame.Utils.Noise
{
    public class WhiteNoise : INoise
    {
        private int Seed { get; init; }

        public WhiteNoise(int seed)
        {
            Seed = seed;
        }

        public float GetValue(float x, float y)
        {
            unchecked
            {
                uint n = (uint)(x + y * 57 + Seed * 131);
                n = (n << 13) ^ n;
                uint t = (n * (n * n * 15731 + 789221) + 1376312589);
                return 1.0f - ((t & 0x7fffffff) / 1073741824f); // [-1,1]
            }
        }
    }
}
