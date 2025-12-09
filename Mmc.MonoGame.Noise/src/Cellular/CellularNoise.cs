using Mmc.MonoGame.Utils;

namespace Mmc.MonoGame.Noise.Cellular
{
    public class CellularNoise : INoise
    {
        protected PseudoRandom PseudoRandom { get; init; }

        protected double CellSize { get; init; }

        protected Func<Vector2d, Vector2d, double> DistanceMetric { get; init; }

        public CellularNoise(int seed, double cellSize = 1, Func<Vector2d, Vector2d, double>? distanceMetric = null)
        {
            PseudoRandom = new PseudoRandom(seed);
            CellSize = cellSize;
            DistanceMetric = distanceMetric ?? Vector2d.Distance;
        }

        public double GetValue(double x, double y)
        {
            x /= CellSize;
            y /= CellSize;

            Vector2d givenPoint = new Vector2d(x, y);

            int xi = (int)x;
            int yi = (int)y;

            double minDistance1 = float.MaxValue;
            double minDistance2 = float.MaxValue;

            // check the the cells in a 3x3 range around the current cell
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int cx = xi + dx;
                    int cy = yi + dy;

                    double xOffset = PseudoRandom.GetRandomValue(cx, cy, 1);
                    double yOffset = PseudoRandom.GetRandomValue(cx, cy, 2);
                    Vector2d pointInC = new Vector2d(cx + xOffset, cy + yOffset);

                    double newDistance = DistanceMetric(pointInC, givenPoint);

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

        protected virtual double CalculateReturnValue(double minDistance1, double minDistance2)
        {
            return minDistance1;
        }
    }
}
