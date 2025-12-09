namespace Mmc.MonoGame.Noise.ColoredNoise
{
    /// <summary>
    /// Gaussian noise is similar to white noise but instead of the values being uniformly distributed on [0,1],
    /// they are normally distributed on [0,1]
    /// </summary>
    public class GaussianWhiteNoise : INoise
    {
        private int Seed { get; init; }

        private double StandardDeviationRange { get; init; }

        public GaussianWhiteNoise(int seed, double standardDevationRange = 3)
        {
            Seed = seed;
            StandardDeviationRange = standardDevationRange;
        }

        public double GetValue(double x, double y)
        {
            unchecked
            {
                uint n = (uint)(x + y * 57 + Seed * 131);
                n = n << 13 ^ n;
                uint t = n * (n * n * 15731 + 789221) + 1376312589;
                double n1 = 1.0f - (t & 0x7fffffff) / 1073741824f; // [-1,1]
                double u1 = (n1 + 1) / 2f; // [0,1]

                n = (uint)(x + y * 67 + Seed * 128);
                n = n << 13 ^ n;
                t = n * (n * n * 19478 + 105787) + 1837561937;
                double n2 = 1.0f - (t & 0x7fffffff) / 1073741824f; // [-1,1]
                double u2 = (n2 + 1) / 2f; // [0,1]


                // Box-Muller Transform https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
                double z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(Math.Tau * u2);

                // apply standard deviation scaling
                z /= StandardDeviationRange;
                return Math.Clamp(z, -1, 1);
            }
        }
    }
}
