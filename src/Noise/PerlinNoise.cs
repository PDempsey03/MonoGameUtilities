using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise
{
    // Implementation based off https://www.youtube.com/watch?v=kCIaHqb60Cw
    public class PerlinNoise
    {
        private int Seed { get; set; }

        private int ZoomFactor { get; set; }

        private int Octaves { get; set; }

        /// <summary>
        /// Construct Instance of Perlin Noise
        /// </summary>
        /// <param name="seed">Defines the random seed used when generating the perlin noise</param>
        /// <param name="zoomFactor">Defines how zoomed in on the perlin noise the output will be. (100-400 is a good starting range)</param>
        /// <param name="octaves">Defines the granularity of the perlin noise. Higher values result in more detail. (4-12 is a good starting range)</param>
        public PerlinNoise(int seed, int zoomFactor, int octaves)
        {
            Seed = seed;
            ZoomFactor = zoomFactor;
            Octaves = octaves;
        }

        public float GetValue(float x, float y)
        {
            float val = 0;

            float frequency = 1;
            float amplitude = 1;

            for (int o = 0; o < Octaves; o++)
            {
                val += InternalPerlinOnOctave(x * frequency / ZoomFactor, y * frequency / ZoomFactor) * amplitude;

                frequency *= 2;
                amplitude /= 2;
            }

            return MathHelper.Clamp(val, -1, 1);
        }

        private float InternalPerlinOnOctave(float x, float y)
        {
            // grid cell corner positions
            int x0 = (int)x;
            int y0 = (int)y;
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            // interpolation weights
            float sx = x - x0;
            float sy = y - y0;

            // compute and interpolate top two corners
            float n0 = DotGridGradient(x0, y0, x, y);
            float n1 = DotGridGradient(x1, y0, x, y);
            float ix0 = CubicInterpolation(n0, n1, sx);

            // compute and interpolate bottom two corners
            n0 = DotGridGradient(x0, y1, x, y);
            n1 = DotGridGradient(x1, y1, x, y);
            float ix1 = CubicInterpolation(n0, n1, sx);

            // interpolate previously interpolated horizontal values vertically to get answer
            float result = CubicInterpolation(ix0, ix1, sy);

            return result;
        }

        private float DotGridGradient(int ix, int iy, float x, float y)
        {
            // get gradient at the provided integer coordinates
            Vector2 gradient = RandomGradient(ix, iy);

            // compute distance vector
            float dx = x - ix;
            float dy = y - iy;

            // compute dot product
            return (dx * gradient.X + dy * gradient.Y);
        }

        private Vector2 RandomGradient(int ix, int iy)
        {
            // unchecked as we desire overflow
            unchecked
            {
                const int w = 8 * sizeof(uint);
                const int s = w / 2;

                uint a = (uint)(ix + Seed * 334875276);
                uint b = (uint)(iy + Seed * 690875635);

                a *= 3284157443u;

                b ^= (a << s) | (a >> (w - s));
                b *= 1911520717u;

                a ^= (b << s) | (b >> (w - s));
                a *= 2048419325u;

                const float invIntMax = 1f / int.MaxValue;

                float angle = (a & 0x7FFFFFFF) * (MathF.Tau * invIntMax);

                return new Vector2(MathF.Sin(angle), MathF.Cos(angle));
            }
        }

        private static float CubicInterpolation(float a0, float a1, float w)
        {
            float temp = (a1 - a0) * (3f - w * 2f) * w * w + a0;
            return temp;
        }
    }
}
