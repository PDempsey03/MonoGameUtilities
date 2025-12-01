using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Noise.ColoredNoise
{
    /// <summary>
    /// Gaussian noise is similar to white noise but instead of the values being uniformly distributed on [0,1],
    /// they are normally distributed on [0,1]
    /// </summary>
    public class GaussianWhiteNoise : INoise
    {
        private int Seed { get; init; }

        private float StandardDeviationRange { get; init; }

        public GaussianWhiteNoise(int seed, float standardDevationRange = 3)
        {
            Seed = seed;
            StandardDeviationRange = standardDevationRange;
        }

        public float GetValue(float x, float y)
        {
            unchecked
            {
                uint n = (uint)(x + y * 57 + Seed * 131);
                n = n << 13 ^ n;
                uint t = n * (n * n * 15731 + 789221) + 1376312589;
                float n1 = 1.0f - (t & 0x7fffffff) / 1073741824f; // [-1,1]
                float u1 = (n1 + 1) / 2f; // [0,1]

                n = (uint)(x + y * 67 + Seed * 128);
                n = n << 13 ^ n;
                t = n * (n * n * 19478 + 105787) + 1837561937;
                float n2 = 1.0f - (t & 0x7fffffff) / 1073741824f; // [-1,1]
                float u2 = (n2 + 1) / 2f; // [0,1]


                // Box-Muller Transform https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
                float z = MathF.Sqrt(-2 * MathF.Log(u1)) * MathF.Cos(MathF.Tau * u2);

                // apply standard deviation scaling
                z /= StandardDeviationRange;
                return MathHelper.Clamp(z, -1, 1);
            }
        }
    }
}
