using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Curves._2D.Geometric
{
    public class EllipticalCurve2D : Curve2D
    {
        /// <summary>
        /// Center of the ellipse.
        /// </summary>
        public Vector2 Center { get; set; }

        /// <summary>
        /// Major and minor radii of the ellipse.
        /// </summary>
        public Vector2 Radius { get; set; }

        /// <summary>
        /// Angle from the horizontal where the first point is (t = 0).
        /// </summary>
        public float StartingAngle { get; set; }

        /// <summary>
        /// Angle from the horizontal where the last point is (t = 1).
        /// </summary>
        public float EndingAngle { get; set; }

        public override bool IsSmooth => true;

        /// <summary>
        /// Initiate a EllipticalCurve2D with literal parameters.
        /// </summary>
        /// <param name="center">Center of the ellipse.</param>
        /// <param name="radius">Major and minor radii of the ellipse.</param>
        /// <param name="startingAngle">Angle from the horizontal where the first point is (t = 0).</param>
        /// <param name="endingAngle">Angle from the horizontal where the last point is (t = 1).</param>
        public EllipticalCurve2D(Vector2 center, Vector2 radius, float startingAngle, float endingAngle)
        {
            Center = center;
            Radius = radius;
            StartingAngle = startingAngle;
            EndingAngle = endingAngle;
        }

        public override Vector2 GetPoint(float t)
        {
            float targetAngle = MathHelper.Lerp(StartingAngle, EndingAngle, t);

            float xOffset = Radius.X * MathF.Cos(MathHelper.ToRadians(targetAngle));
            float yOffset = Radius.Y * MathF.Sin(MathHelper.ToRadians(targetAngle));

            return Center + new Vector2(xOffset, yOffset);
        }

        public override Vector2 GetTangent(float t)
        {
            float targetAngle = MathHelper.Lerp(StartingAngle, EndingAngle, t);

            float dx = MathF.Cos(MathHelper.ToRadians(targetAngle));
            float dy = MathF.Sin(MathHelper.ToRadians(targetAngle));

            return new Vector2(-dy, dx);
        }
    }
}
