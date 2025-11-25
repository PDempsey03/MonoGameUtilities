namespace Mmc.MonoGame.Utils.Noise.ColoredNoise
{
    public class WhiteNoise : INoise
    {
        private PseudoRandom PseudoRandom { get; init; }

        public WhiteNoise(int seed)
        {
            PseudoRandom = new PseudoRandom(seed);
        }

        public float GetValue(float x, float y)
        {
            return PseudoRandom.GetRandomValue(x, y) * 2 - 1; // ensure range is [-1, 1]
        }
    }
}
