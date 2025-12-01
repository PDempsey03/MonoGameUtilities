using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Curves._2D.Polynomial
{
    public class LinearCurve2D : Curve2D
    {
        /// <summary>
        /// Start of the curve (t = 0).
        /// </summary>
        public Vector2 Start { get; set; }

        /// <summary>
        /// End of the curve (t = 1).
        /// </summary>
        public Vector2 End { get; set; }

        public override bool IsSmooth => true;

        /// <summary>
        /// Initialize new LinearCurve2D with start and end points.
        /// </summary>
        /// <param name="start">Start of the curve (t = 0)</param>
        /// <param name="end">End of the curve (t = 1)</param>
        public LinearCurve2D(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Initialize new LinearCurve2D with alternate parameters.
        /// </summary>
        /// <param name="slope">Slope of the line, from right to left.</param>
        /// <param name="length">Total length of the line from start to end.</param>
        /// <param name="middlePoint">Point on the line where t = 0.5.</param>
        /// <param name="leftSideIsStart">Whether or not the left point is t = 0 or t = 1.</param>
        public LinearCurve2D(float slope, float length, Vector2 middlePoint, bool leftSideIsStart = true)
        {
            Vector2 normalizedSlope = Vector2.Normalize(new Vector2(1, slope));
            float halfLength = length / 2;
            Vector2 scaledSlope = halfLength * normalizedSlope;

            Vector2 localStart = middlePoint - scaledSlope;
            Vector2 localEnd = middlePoint + scaledSlope;

            if (leftSideIsStart)
            {
                Start = localStart;
                End = localEnd;
            }
            else
            {
                Start = localEnd;
                End = localStart;
            }
        }

        public override Vector2 GetPoint(float t)
        {
            return Vector2.Lerp(Start, End, t);
        }

        public override Vector2 GetTangent(float t)
        {
            return End - Start;
        }
    }
}
