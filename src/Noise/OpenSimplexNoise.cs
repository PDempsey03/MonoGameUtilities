using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise
{
    public class OpenSimplexNoise : INoise
    {
        private int Seed { get; init; }

        private float ZoomFactor { get; init; }

        private int Octaves { get; init; }

        private float Lacunarity { get; init; }

        private float Gain { get; init; }

        public OpenSimplexNoise(int seed, float zoomFactor = 100, int octaves = 1, float lacunarity = 2, float gain = .5f)
        {
            Seed = seed;
            ZoomFactor = zoomFactor;
            Octaves = octaves;
            Lacunarity = lacunarity;
            Gain = gain;
        }

        public float GetValue(float x, float y)
        {
            float val = 0;

            float frequency = 1;
            float amplitude = 1;

            for (int o = 0; o < Octaves; o++)
            {
                val += InternalOpenSimplexOnOctave(x * frequency / ZoomFactor, y * frequency / ZoomFactor) * amplitude;

                frequency *= Lacunarity;
                amplitude *= Gain;
            }

            return MathHelper.Clamp(val, -1, 1);
        }

        private float InternalOpenSimplexOnOctave(float x, float y)
        {
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
            return MathHelper.Clamp(70.0f * (n0 + n1 + n2), -1f, 1f);
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
            var grad = RandomGradient(i, j);

            // return dot product with t^2 as a smoothness since interpolation isn't done anywhere else
            return t * t * (grad.X * x + grad.Y * y);
        }

        private Vector2 RandomGradient(int ix, int iy)
        {
            unchecked
            {
                uint a = (uint)(ix + Seed * 334875276);
                uint b = (uint)(iy + Seed * 690875635);

                const int w = 32;
                const int s = 16;

                a *= 3284157443u;
                b ^= (a << s) | (a >> (w - s));
                b *= 1911520717u;
                a ^= (b << s) | (b >> (w - s));
                a *= 2048419325u;

                float angle = (a & 0x7FFFFFFF) / (float)int.MaxValue * MathF.Tau;
                return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            }
        }

        private static int IntegerFloor(float x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }
    }
}
