using Mmc.MonoGame.Utils;

namespace Mmc.MonoGame.Noise.Cellular
{
    public class SecondOrderCellularNoise : CellularNoise
    {
        public SecondOrderCellularNoise(int seed, double cellSize = 1, Func<Vector2d, Vector2d, double>? distanceMetric = null) : base(seed, cellSize, distanceMetric)
        {

        }

        protected override double CalculateReturnValue(double minDistance1, double minDistance2)
        {
            return minDistance2;
        }
    }
}
