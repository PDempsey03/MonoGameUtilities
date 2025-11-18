using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise
{
    public class ValueNoise : INoise
    {
        private int Seed { get; init; }

        private int ZoomFactor { get; init; }

        private int Octaves { get; init; }

        private float Lacunarity { get; init; }

        private float Gain { get; init; }

        public ValueNoise(int seed, int zoomFactor = 100, int octaves = 1, float lacunarity = 2, float gain = .5f)
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
                val += InternalValueOnOctave(x * frequency / ZoomFactor, y * frequency / ZoomFactor) * amplitude;

                frequency *= Lacunarity;
                amplitude *= Gain;
            }

            return MathHelper.Clamp(val, -1, 1);
        }

        private float InternalValueOnOctave(float x, float y)
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
            float n0 = RandomGridValue(x0, y0);
            float n1 = RandomGridValue(x1, y0);
            float ix0 = CubicInterpolation(n0, n1, sx);

            // compute and interpolate bottom two corners
            n0 = RandomGridValue(x0, y1);
            n1 = RandomGridValue(x1, y1);
            float ix1 = CubicInterpolation(n0, n1, sx);

            // interpolate previously interpolated horizontal values vertically to get answer
            float result = CubicInterpolation(ix0, ix1, sy);

            return result;
        }

        private float RandomGridValue(int ix, int iy)
        {
            // unchecked as we desire overflow
            unchecked
            {
                uint n = (uint)(ix + iy * 75 + Seed * 131);
                n = n << 13 ^ n;
                uint t = n * (n * n * 15911 + 798731) + 1396473869;
                return 1.0f - (t & 0x7fffffff) / 1073741824f; // [-1,1]
            }
        }

        private static float CubicInterpolation(float a0, float a1, float w)
        {
            return (a1 - a0) * (3f - w * 2f) * w * w + a0;
        }
    }
}