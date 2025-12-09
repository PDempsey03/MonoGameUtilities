using Mmc.MonoGame.Utils;

namespace Mmc.MonoGame.Noise.ColoredNoise
{
    public class WhiteNoise : INoise
    {
        private PseudoRandom PseudoRandom { get; init; }

        public WhiteNoise(int seed)
        {
            PseudoRandom = new PseudoRandom(seed);
        }

        public double GetValue(double x, double y)
        {
            return PseudoRandom.GetRandomValue(x, y) * 2 - 1; // ensure range is [-1, 1]
        }
    }
}
