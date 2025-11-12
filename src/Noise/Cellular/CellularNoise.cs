using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise.Cellular
{
    public class CellularNoise : INoise
    {
        protected int Seed { get; init; }

        protected float CellSize { get; init; }

        protected Func<Vector2, Vector2, float> DistanceMetric { get; init; }

        public CellularNoise(int seed, float cellSize = 1, Func<Vector2, Vector2, float>? distanceMetric = null)
        {
            Seed = seed;
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

                    Vector2 pointInC = RandomPointInCell(cx, cy);

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

        private Vector2 RandomPointInCell(int cx, int cy)
        {
            // unchecked as we desire overflow
            unchecked
            {
                int h = cx;
                h = h * 374761393 ^ cy * 668265263;
                h = (h ^ Seed * 1446649773) * 1274126177;
                h ^= h >> 13;

                float rx = (h & 0xFF) / 255f; // use last 8 bits
                float ry = (h >> 8 & 0xFF) / 255f; // use next 8 bits
                return new Vector2(cx + rx, cy + ry);
            }
        }
    }
}
