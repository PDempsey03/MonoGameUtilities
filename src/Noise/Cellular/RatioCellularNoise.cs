using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise.Cellular
{
    public class RatioCellularNoise : CellularNoise
    {
        public RatioCellularNoise(int seed, float cellSize = 1, Func<Vector2, Vector2, float>? distanceMetric = null) : base(seed, cellSize, distanceMetric)
        {

        }

        protected override float CalculateReturnValue(float minDistance1, float minDistance2)
        {
            return minDistance1 / minDistance2;
        }
    }
}
