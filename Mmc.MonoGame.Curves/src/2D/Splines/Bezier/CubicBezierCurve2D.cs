using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Curves._2D.Splines.Bezier
{
    public class CubicBezierCurve2D : Curve2D
    {
        public override bool IsSmooth => true;

        /// <summary>
        /// Start point of the curve.
        /// </summary>
        public Vector2 P0 { get; set; }

        /// <summary>
        /// First control point of the curve.
        /// </summary>
        public Vector2 P1 { get; set; }

        /// <summary>
        /// Second control point of the curve.
        /// </summary>
        public Vector2 P2 { get; set; }

        /// <summary>
        /// End Point of the curve.
        /// </summary>
        public Vector2 P3 { get; set; }

        /// <summary>
        /// Creates a new cubic Bezier curve with the specified points.
        /// </summary>
        /// <param name="p0">The start point of the curve.</param>
        /// <param name="p1">The first control point of the curve.</param>
        /// <param name="p2">The second control point of the curve.</param>
        /// <param name="p3">The end Point of the curve.</param>
        public CubicBezierCurve2D(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public override Vector2 GetPoint(float t)
        {
            Vector2 P0P1Lerp = Vector2.Lerp(P0, P1, t);
            Vector2 P1P2Lerp = Vector2.Lerp(P1, P2, t);
            Vector2 P2P3Lerp = Vector2.Lerp(P2, P3, t);

            Vector2 Q0Q1Lerp = Vector2.Lerp(P0P1Lerp, P1P2Lerp, t);
            Vector2 Q1Q2Lerp = Vector2.Lerp(P1P2Lerp, P2P3Lerp, t);

            Vector2 finalLerp = Vector2.Lerp(Q0Q1Lerp, Q1Q2Lerp, t);

            return finalLerp;
        }

        public override Vector2 GetTangent(float t)
        {
            Vector2 a = P1 - P0;
            Vector2 b = P2 - P1;
            Vector2 c = P3 - P2;

            Vector2 term1 = 3f * (1f - t) * (1f - t) * a;
            Vector2 term2 = 6f * (1f - t) * t * b;
            Vector2 term3 = 3f * t * t * c;

            return term1 + term2 + term3;
        }
    }
}
