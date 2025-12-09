namespace Mmc.MonoGame.Noise.Transforms
{
    public class DynamicDomainRotateNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private INoise AngleNoise { get; init; }

        private double MaxRotationAngleRadians { get; init; }

        private double XPivot { get; init; }

        private double YPivot { get; init; }

        public DynamicDomainRotateNoise(INoise sourceNoise, INoise angleNoise, double maxRotationAngleRadians = Math.PI / 2, double xPivot = 0, double yPivot = 0)
        {
            SourceNoise = sourceNoise;
            AngleNoise = angleNoise;
            MaxRotationAngleRadians = maxRotationAngleRadians;
            XPivot = xPivot;
            YPivot = yPivot;
        }

        public double GetValue(double x, double y)
        {
            double px = x - XPivot;
            double py = y - YPivot;

            double trueAngle = AngleNoise.GetValue(x, y) * MaxRotationAngleRadians;

            double cos = Math.Cos(trueAngle);
            double sin = Math.Sin(trueAngle);

            double rotatedX = px * cos - py * sin;
            double rotatedY = px * sin + py * cos;

            rotatedX += XPivot;
            rotatedY += YPivot;

            return SourceNoise.GetValue(rotatedX, rotatedY);
        }
    }
}
