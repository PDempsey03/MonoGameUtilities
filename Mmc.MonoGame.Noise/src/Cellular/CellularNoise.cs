using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils;

namespace Mmc.MonoGame.Noise.Cellular
{
    public class CellularNoise : INoise
    {
        protected PseudoRandom PseudoRandom { get; init; }

        protected float CellSize { get; init; }

        protected Func<Vector2, Vector2, float> DistanceMetric { get; init; }

        public CellularNoise(int seed, float cellSize = 1, Func<Vector2, Vector2, float>? distanceMetric = null)
        {
            PseudoRandom = new PseudoRandom(seed);
            CellSize = cellSize;
            DistanceMetric = distanceMetric ?? Vector2.Distance;
        }

        public float GetValue(float x, float y)
        {
            x /= CellSize;
            y /= CellSize;

            Vector2 givenPoint = new Vector2(x, y);

            int xi = (int)x;
            int yi = (int)y;

            float minDistance1 = float.MaxValue;
            float minDistance2 = float.MaxValue;

            // check the the cells in a 3x3 range around the current cell
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int cx = xi + dx;
                    int cy = yi + dy;

                    float xOffset = PseudoRandom.GetRandomValue(cx, cy, 1);
                    float yOffset = PseudoRandom.GetRandomValue(cx, cy, 2);
                    Vector2 pointInC = new Vector2(cx + xOffset, cy + yOffset);

                    float newDistance = DistanceMetric(pointInC, givenPoint);

                    if (newDistance < minDistance1)
                    {
                        minDistance2 = minDistance1;
                        minDistance1 = newDistance;
                    }
                    else if (newDistance < minDistance2)
                    {
                        minDistance2 = newDistance;
                    }
                }
            }

            return CalculateReturnValue(minDistance1, minDistance2) * CellSize;
        }

        protected virtual float CalculateReturnValue(float minDistance1, float minDistance2)
        {
            return minDistance1;
        }
    }
}
