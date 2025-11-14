namespace Mmc.MonoGame.Utils.Noise.Combiners
{
    public class InvertNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        public InvertNoise(INoise sourceNoise)
        {
            SourceNoise = sourceNoise;
        }

        public float GetValue(float x, float y)
        {
            return -SourceNoise.GetValue(x, y);
        }
    }
}
