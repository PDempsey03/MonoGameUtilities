namespace Mmc.MonoGame.Noise.Transforms
{
    public class RotateNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private float AngleRadians { get; init; }

        private float XPivot { get; init; }

        private float YPivot { get; init; }

        private bool ClockWiseRotation { get; init; }

        public RotateNoise(INoise sourceNoise, float angleRadians, float xPivot = 0, float yPivot = 0, bool clockWiseRotation = false)
        {
            SourceNoise = sourceNoise;
            AngleRadians = angleRadians;
            XPivot = xPivot;
            YPivot = yPivot;
            ClockWiseRotation = clockWiseRotation;
        }

        public float GetValue(float x, float y)
        {
            float px = x - XPivot;
            float py = y - YPivot;

            float trueAngle = ClockWiseRotation ? -AngleRadians : AngleRadians;

            float cos = MathF.Cos(trueAngle);
            float sin = MathF.Sin(trueAngle);

            float rotatedX = px * cos - py * sin;
            float rotatedY = px * sin + py * cos;

            rotatedX += XPivot;
            rotatedY += YPivot;

            return SourceNoise.GetValue(rotatedX, rotatedY);
        }
    }
}
