using Mmc.MonoGame.Utils;

namespace Mmc.MonoGame.Noise
{
    public class ValueNoise : INoise
    {
        private PseudoRandom PseudoRandom { get; init; }

        private int ZoomFactor { get; init; }

        public ValueNoise(int seed, int zoomFactor = 100)
        {
            PseudoRandom = new PseudoRandom(seed);
            ZoomFactor = zoomFactor;
        }

        public float GetValue(float x, float y)
        {
            x /= ZoomFactor;
            y /= ZoomFactor;

            // grid cell corner positions
            int x0 = (int)x;
            int y0 = (int)y;
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            // interpolation weights
            float sx = x - x0;
            float sy = y - y0;

            // compute and interpolate top two corners
            float n0 = PseudoRandom.GetRandomValueInRange(-1, 1, x0, y0);
            float n1 = PseudoRandom.GetRandomValueInRange(-1, 1, x1, y0);
            float ix0 = CubicInterpolation(n0, n1, sx);

            // compute and interpolate bottom two corners
            n0 = PseudoRandom.GetRandomValueInRange(-1, 1, x0, y1);
            n1 = PseudoRandom.GetRandomValueInRange(-1, 1, x1, y1);
            float ix1 = CubicInterpolation(n0, n1, sx);

            // interpolate previously interpolated horizontal values vertically to get answer
            float result = CubicInterpolation(ix0, ix1, sy);

            return result;
        }

        private static float CubicInterpolation(float a0, float a1, float w)
        {
            return (a1 - a0) * (3f - w * 2f) * w * w + a0;
        }
    }
}