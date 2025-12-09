namespace Mmc.MonoGame.Noise.Transforms
{
    public class RotateNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private double AngleRadians { get; init; }

        private double XPivot { get; init; }

        private double YPivot { get; init; }

        private bool ClockWiseRotation { get; init; }

        public RotateNoise(INoise sourceNoise, double angleRadians, double xPivot = 0, double yPivot = 0, bool clockWiseRotation = false)
        {
            SourceNoise = sourceNoise;
            AngleRadians = angleRadians;
            XPivot = xPivot;
            YPivot = yPivot;
            ClockWiseRotation = clockWiseRotation;
        }

        public double GetValue(double x, double y)
        {
            double px = x - XPivot;
            double py = y - YPivot;

            double trueAngle = ClockWiseRotation ? -AngleRadians : AngleRadians;

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
