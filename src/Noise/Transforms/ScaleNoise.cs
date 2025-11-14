namespace Mmc.MonoGame.Utils.Noise.Transforms
{
    public class ScaleNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private float XScale { get; init; }

        private float YScale { get; init; }

        public ScaleNoise(INoise sourceNoise, float xScale, float yScale)
        {
            SourceNoise = sourceNoise;
            XScale = xScale;
            YScale = yScale;
        }

        public float GetValue(float x, float y)
        {
            return SourceNoise.GetValue(x * XScale, y * YScale);
        }
    }
}
