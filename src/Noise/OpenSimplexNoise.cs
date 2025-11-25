using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise
{
    public class OpenSimplexNoise : INoise
    {
        private const float EmpiricalScalingConstant = 70f / (1.41421356237f / 2f);

        private PseudoRandom PseudoRandom { get; init; }

        private float ZoomFactor { get; init; }

        public OpenSimplexNoise(int seed, float zoomFactor = 100)
        {
            PseudoRandom = new PseudoRandom(seed);
            ZoomFactor = zoomFactor;
        }

        public float GetValue(float x, float y)
        {
            x /= ZoomFactor;
            y /= ZoomFactor;

            // skewing factors for triangular space
            const float F2 = 0.366025403f; // (sqrt(3)-1)/2
            const float G2 = 0.211324865f; // (3-sqrt(3))/6

            // skew input space to determine which simplex cell we're in
            float s = (x + y) * F2;
            int i = IntegerFloor(x + s);
            int j = IntegerFloor(y + s);

            // unskewed lattice cell origin
            float t = (i + j) * G2;

            // corner 0 position in unskewed cartesian space
            float X0 = i - t;
            float Y0 = j - t;

            // distance to corner 0
            float x0 = x - X0;
            float y0 = y - Y0;

            // determine which of the triangles we are in (2 triangles are contained in each skewed square grid cell)
            int i1, j1;
            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            }
            else
            {
                i1 = 0;
                j1 = 1;
            }

            // distance to corner 1 and 2
            float x1 = x0 - i1 + G2;
            float y1 = y0 - j1 + G2;
            float x2 = x0 - 1 + 2 * G2;
            float y2 = y0 - 1 + 2 * G2;

            // get the contribution of each corner on the triangle
            float n0 = CornerContribution(i, j, x0, y0);
            float n1 = CornerContribution(i + i1, j + j1, x1, y1);
            float n2 = CornerContribution(i + 1, j + 1, x2, y2);

            // Scale result to [-1,1]
            return MathHelper.Clamp(EmpiricalScalingConstant * (n0 + n1 + n2), -1f, 1f);
        }

        private float CornerContribution(int i, int j, float x, float y)
        {
            // radius of influence is .5^2
            // calculates squared distance to corner
            float t = 0.5f - x * x - y * y;
            if (t < 0) return 0f;

            // get true distance 
            t *= t;

            // get the gradient at the corner
            float angle = PseudoRandom.GetRandomValueInRange(0, MathF.Tau, i, j);
            var grad = new Vector2(MathF.Cos(angle), MathF.Sin(angle));

            // return dot product with t^2 as a smoothness since interpolation isn't done anywhere else
            return t * t * (grad.X * x + grad.Y * y);
        }

        private static int IntegerFloor(float x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }
    }
}
