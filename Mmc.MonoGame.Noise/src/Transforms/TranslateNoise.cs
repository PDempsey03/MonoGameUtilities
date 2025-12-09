namespace Mmc.MonoGame.Noise.Transforms
{
    public class TranslateNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private double XTrans { get; init; }

        private double YTrans { get; init; }

        public TranslateNoise(INoise sourceNoise, double xTrans, double yTrans)
        {
            SourceNoise = sourceNoise;
            XTrans = xTrans;
            YTrans = yTrans;
        }

        public double GetValue(double x, double y)
        {
            return SourceNoise.GetValue(x + XTrans, y + YTrans);
        }
    }
}
