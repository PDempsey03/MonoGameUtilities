namespace Mmc.MonoGame.Noise.Transforms
{
    public class TranslateNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private float XTrans { get; init; }

        private float YTrans { get; init; }

        public TranslateNoise(INoise sourceNoise, float xTrans, float yTrans)
        {
            SourceNoise = sourceNoise;
            XTrans = xTrans;
            YTrans = yTrans;
        }

        public float GetValue(float x, float y)
        {
            return SourceNoise.GetValue(x + XTrans, y + YTrans);
        }
    }
}
