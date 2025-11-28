using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Curves._2D.Splines
{
    /// <summary>
    /// Spline which interpolates between all points with cubic functions defined by derivative values at each point.
    /// </summary>
    public class CubicHermiteSplineCurve2D : CompoundCurve2D
    {
        /// <summary>
        /// Initialize new curve by taking derivative values and scaling them to be Vector2's of magnitude 1
        /// </summary>
        /// <param name="points">Points on the curve</param>
        /// <param name="derivatives">Derivative values at points which will be given magnitude 1</param>
        public CubicHermiteSplineCurve2D(Vector2[] points, float[] derivatives)
        {
            InitializeToValues(points, derivatives.Select(d => Vector2.Normalize(new Vector2(1, d))).ToArray());
        }

        /// <summary>
        /// Initialize new curve with raw values.
        /// </summary>
        /// <param name="points">Points on the curve</param>
        /// <param name="tangents">Raw tangent values at each point</param>
        public CubicHermiteSplineCurve2D(Vector2[] points, Vector2[] tangents)
        {
            InitializeToValues(points, tangents);
        }

        /// <summary>
        /// Resets and updates curve definition to new parameters.
        /// </summary>
        /// <param name="points">Points on the curve</param>
        /// <param name="tangents">Raw tangent values at each point</param>
        public virtual void InitializeToValues(Vector2[] points, Vector2[] tangents)
        {
            if (points.Length < 2) return; // cant do 1 point

            Curves.Clear();

            int pointCount = points.Length;
            int tangentCount = tangents.Length;

            Vector2 curTangent = tangentCount > 0 ? tangents[0] : points[1] - points[0];
            Vector2 nextTangent;

            int i = 0;
            do
            {
                nextTangent = i + 1 < tangentCount ? tangents[i + 1] : (points[i + 2] - points[i]) / 2;

                Curves.Add(new CubicHermiteSegment(points[i], points[i + 1], curTangent, nextTangent));

                curTangent = nextTangent;
                i++;
            } while (i < pointCount - 2);

            nextTangent = i + 1 < tangentCount ? tangents[i + 1] : points[i + 1] - points[i];
            Curves.Add(new CubicHermiteSegment(points[i], points[i + 1], curTangent, nextTangent));
        }

        /// <summary>
        /// Internal representation of each segment of the cubic hermite spline
        /// </summary>
        private class CubicHermiteSegment : Curve2D
        {
            private Vector2 P0 { get; set; }

            private Vector2 P1 { get; set; }

            private Vector2 M0 { get; set; }

            private Vector2 M1 { get; set; }

            public override bool IsSmooth => true;

            public CubicHermiteSegment(Vector2 p0, Vector2 p1, Vector2 m0, Vector2 m1)
            {
                P0 = p0;
                P1 = p1;
                M0 = m0;
                M1 = m1;
            }

            public override Vector2 GetPoint(float t)
            {
                float t2 = t * t;
                float t3 = t2 * t;

                float h00 = 2 * t3 - 3 * t2 + 1;
                float h10 = t3 - 2 * t2 + t;
                float h01 = -2 * t3 + 3 * t2;
                float h11 = t3 - t2;

                return h00 * P0 + h10 * M0 + h01 * P1 + h11 * M1;
            }

            public override Vector2 GetTangent(float t)
            {
                float t2 = t * t;

                float dH00 = 6 * t2 - 6 * t;
                float dH10 = 3 * t2 - 4 * t + 1;
                float dH01 = -6 * t2 + 6 * t;
                float dH11 = 3 * t2 - 2 * t;

                return dH00 * P0 + dH10 * M0 + dH01 * P1 + dH11 * M1;
            }
        }
    }
}
