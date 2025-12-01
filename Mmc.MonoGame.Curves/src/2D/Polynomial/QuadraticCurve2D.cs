using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Curves._2D.Polynomial
{
    /// <summary>
    /// Quadratic class is defined by the parametric equation x(t) = A.X * t^2 + B.x * t + C.x and y(t) = A.Y * t^2 + B.Y * t + C.Y
    /// </summary>
    public class QuadraticCurve2D : Curve2D
    {
        /// <summary>
        /// Coefficient in front of the t^2 term.
        /// </summary>
        public Vector2 A { get; set; }

        /// <summary>
        /// Coefficient in front of the t term.
        /// </summary>
        public Vector2 B { get; set; }

        /// <summary>
        /// Constant term.
        /// </summary>
        public Vector2 C { get; set; }

        public override bool IsSmooth => true;

        /// <summary>
        /// Instantiate new QuadraticCurve2D with literal parameters.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public QuadraticCurve2D(Vector2 a, Vector2 b, Vector2 c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Instantiate new QuadraticCurve2D given 3 points on the curve with where the extra point should be along the curve
        /// </summary>
        /// <param name="start">Point on parabola where t = 0.</param>
        /// <param name="end">Point on the parabola where t = 1.</param>
        /// <param name="other">Point on the parabola where t = <param name="otherT">.</param>
        /// <param name="otherT">How far along the curve the other point should be.</param>
        public QuadraticCurve2D(Vector2 start, Vector2 end, Vector2 other, float otherT)
        {
            if (otherT <= 0 || otherT >= 1)
                throw new ArgumentException($"{nameof(otherT)} must be between 0 and 1 (exclusive).", nameof(otherT));

            float otherTSquared = otherT * otherT;

            C = start;

            var bNum = (end - start) * (otherTSquared) + start - other;
            var bDenom = otherTSquared - otherT;

            B = bNum / bDenom;

            A = end - B - start;
        }

        public override Vector2 GetPoint(float t)
        {
            return A * (t * t) + B * t + C;
        }

        public override Vector2 GetTangent(float t)
        {
            return 2 * A * t + B;
        }
    }
}
