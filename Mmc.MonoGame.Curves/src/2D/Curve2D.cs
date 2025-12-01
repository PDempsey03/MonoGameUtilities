using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Curves._2D
{
    /// <summary>
    /// 2D curve definition
    /// </summary>
    public abstract class Curve2D
    {
        /// <summary>
        /// Rough definition of whether or not a curve is smooth. Smooth not being the rigourous math definition.
        /// </summary>
        public abstract bool IsSmooth { get; }

        /// <summary>
        /// Get point on the curve corresponding to the given parametric value.
        /// </summary>
        /// <param name="t">Normalized parametric parameter in the range [0,1], where 0 is the start and 1 is the end.</param>
        /// <returns>The point on the curve at the specified parametric position.</returns>
        public abstract Vector2 GetPoint(float t);

        /// <summary>
        /// Get tangent vector at the point on the curve corresponding to the given parametric value.
        /// </summary>
        /// <param name="t">Normalized parametric parameter in the range [0,1], where 0 is the start and 1 is the end.</param>
        /// <returns>The tangent vector at the point on the curve at the specified parametric position.</returns>
        public abstract Vector2 GetTangent(float t);

        /// <summary>
        /// Get normal vector at the point on the curve corresponding to the given parametric value.
        /// </summary>
        /// <param name="t">Normalized parametric parameter in the range [0,1], where 0 is the start and 1 is the end.</param>
        /// <param name="clockwise">Parameter defining which direction the tangent will rotate to get the normal.</param>
        /// <returns>The normal vector at the point on the curve at the specified parametric position.</returns>
        public virtual Vector2 GetNormal(float t, bool clockwise = true)
        {
            Vector2 tangent = GetTangent(t);

            if (tangent == Vector2.Zero) return Vector2.Zero;

            tangent.Normalize();

            return clockwise
                ? new Vector2(tangent.Y, -tangent.X)
                : new Vector2(-tangent.Y, tangent.X);
        }

        /// <summary>
        /// Approiximate the length of the curve by calculating linear distance between sample points.
        /// </summary>
        /// <param name="samples">Amount of sample points used to calculate length.</param>
        /// <returns>Approximate length of the curve.</returns>
        public virtual float GetLength(int samples = 25)
        {
            float length = 0;

            Vector2 prev = GetPoint(0);
            for (int i = 1; i < samples; i++)
            {
                float t = i / samples;
                Vector2 curr = GetPoint(t);
                length += Vector2.Distance(prev, curr);
                prev = curr;
            }

            return length;
        }

        /// <summary>
        /// Get <paramref name="numPoints"/> points along the curve, with evenly spaced out t values.
        /// </summary>
        /// <param name="numPoints">Amount of points along the curve.</param>
        /// <returns><paramref name="numPoints"/> along the curve.</returns>
        public virtual Vector2[] GetPoints(int numPoints)
        {
            Vector2[] points = new Vector2[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (numPoints - 1f);
                points[i] = GetPoint(t);
            }

            return points;
        }

        /// <summary>
        /// Get <paramref name="numPoints"/> points along the curve, with evenly spaced out distance along the curve values.
        /// </summary>
        /// <param name="numPoints">Amount of points along the curve.</param>
        /// <param name="superSamplingSize">Amount of points to perform arc length re-parameterization</param>
        /// <returns><paramref name="numPoints"/> along the curve.</returns>
        public virtual Vector2[] GetEvenlySpacedPoints(int numPoints, int superSamplingSize = 1024)
        {
            // Uses the arc length reparameterization technique

            // 1. Run super sampling to approximate arc length
            Vector2[] superSampling = GetPoints(superSamplingSize);

            // 2. calculate cumulative distances
            List<float> normalizedCumulativeDistances = new List<float>(superSamplingSize)
            {
                0
            };

            float runningCumulativeDistance = 0;
            for (int i = 1; i < superSamplingSize; i++)
            {
                runningCumulativeDistance += Vector2.Distance(superSampling[i], superSampling[i - 1]);
                normalizedCumulativeDistances.Add(runningCumulativeDistance);
            }

            // 3. normalize distances so that they are [0,1] and can be used as the new parameter value
            for (int i = 0; i < superSamplingSize; i++)
            {
                normalizedCumulativeDistances[i] /= runningCumulativeDistance;
            }

            // 4. Use the cumulative distances to get the desired amount of evenly spaced points
            Vector2[] result = new Vector2[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                float t = i / (numPoints - 1f);

                int index = normalizedCumulativeDistances.BinarySearch(t);
                if (index < 0)
                {
                    index = ~index;
                }

                if (index == 0)
                {
                    result[i] = superSampling[i];
                }
                else
                {
                    // 4.5. Since it is almost certain it will land between 2 super sampled points, lerp between them for a more precise value
                    float d0 = normalizedCumulativeDistances[index - 1]; // this one is behind it
                    float d1 = normalizedCumulativeDistances[index]; // this one is in front of it

                    float firstT = (float)(index - 1) / (superSamplingSize - 1);
                    float secondT = (float)index / (superSamplingSize - 1);
                    float lerpT = (t - d0) / (d1 - d0);

                    float finalT = MathHelper.Lerp(firstT, secondT, lerpT);

                    result[i] = GetPoint(finalT);
                }
            }

            return result;
        }
    }
}
