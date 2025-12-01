using Microsoft.Xna.Framework;
using Mmc.MonoGame.Curves._2D.Polynomial;

namespace Mmc.MonoGame.Curves._2D.Splines.Bezier
{
    public class QuadraticBezierCurve2D : Curve2D
    {
        /// <summary>
        /// Start point of the curve
        /// </summary>
        public Vector2 P0 { get; set; }

        /// <summary>
        /// Control point of the curve
        /// </summary>
        public Vector2 P1 { get; set; }

        /// <summary>
        /// End point of the curve
        /// </summary>
        public Vector2 P2 { get; set; }

        public override bool IsSmooth => true;

        /// <summary>
        /// Creates a new quadratic Bezier curve with the specified points.
        /// </summary>
        /// <param name="p0">The start point of the curve.</param>
        /// <param name="p1">The control point of the curve.</param>
        /// <param name="p2">The end point of the curve.</param>
        public QuadraticBezierCurve2D(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            P0 = p0;
            P1 = p1;
            P2 = p2;
        }

        public override Vector2 GetPoint(float t)
        {
            Vector2 P0P1Lerp = Vector2.Lerp(P0, P1, t);
            Vector2 P1P2Lerp = Vector2.Lerp(P1, P2, t);

            Vector2 finalLerp = Vector2.Lerp(P0P1Lerp, P1P2Lerp, t);

            return finalLerp;
        }

        public override Vector2 GetTangent(float t)
        {
            Vector2 p0Comp = -2 * t * P0;
            Vector2 p1Comp = 2 * (1 - 2 * t) * P1;
            Vector2 p2Comp = 2 * t * P2;

            Vector2 total = p0Comp + p1Comp + p2Comp;

            return total;
        }

        /// <summary>
        /// Convert to a a QuadraticCurve2D.
        /// </summary>
        /// <returns>QuadraticCurve2D with identical curve.</returns>
        public QuadraticCurve2D ToQuadraticCurve2D()
        {
            const float LerpValue = .5f;

            var start = GetPoint(0);
            var end = GetPoint(1);
            var middle = GetPoint(LerpValue);

            var result = new QuadraticCurve2D(start, end, middle, LerpValue);

            return result;
        }
    }
}
