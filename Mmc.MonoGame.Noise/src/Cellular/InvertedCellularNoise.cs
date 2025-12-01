using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Noise.Cellular
{
    public class InvertedCellularNoise : CellularNoise
    {
        public InvertedCellularNoise(int seed, float cellSize = 1, Func<Vector2, Vector2, float>? distanceMetric = null) : base(seed, cellSize, distanceMetric)
        {

        }

        protected override float CalculateReturnValue(float minDistance1, float minDistance2)
        {
            return 1 - minDistance1;
        }
    }
}
