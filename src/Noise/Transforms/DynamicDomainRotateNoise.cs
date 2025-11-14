namespace Mmc.MonoGame.Utils.Noise.Transforms
{
    public class DynamicDomainRotateNoise : INoise
    {
        private INoise SourceNoise { get; init; }

        private INoise AngleNoise { get; init; }

        private float MaxRotationAngleRadians { get; init; }

        private float XPivot { get; init; }

        private float YPivot { get; init; }

        public DynamicDomainRotateNoise(INoise sourceNoise, INoise angleNoise, float maxRotationAngleRadians = MathF.PI / 2, float xPivot = 0, float yPivot = 0)
        {
            SourceNoise = sourceNoise;
            AngleNoise = angleNoise;
            MaxRotationAngleRadians = maxRotationAngleRadians;
            XPivot = xPivot;
            YPivot = yPivot;
        }

        public float GetValue(float x, float y)
        {
            float px = x - XPivot;
            float py = y - YPivot;

            float trueAngle = AngleNoise.GetValue(x, y) * MaxRotationAngleRadians;

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
