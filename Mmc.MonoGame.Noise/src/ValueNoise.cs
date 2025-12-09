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

        public double GetValue(double x, double y)
        {
            x /= ZoomFactor;
            y /= ZoomFactor;

            // grid cell corner positions
            int x0 = (int)x;
            int y0 = (int)y;
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            // interpolation weights
            double sx = x - x0;
            double sy = y - y0;

            // compute and interpolate top two corners
            double n0 = PseudoRandom.GetRandomValueInRange(-1, 1, x0, y0);
            double n1 = PseudoRandom.GetRandomValueInRange(-1, 1, x1, y0);
            double ix0 = CubicInterpolation(n0, n1, sx);

            // compute and interpolate bottom two corners
            n0 = PseudoRandom.GetRandomValueInRange(-1, 1, x0, y1);
            n1 = PseudoRandom.GetRandomValueInRange(-1, 1, x1, y1);
            double ix1 = CubicInterpolation(n0, n1, sx);

            // interpolate previously interpolated horizontal values vertically to get answer
            double result = CubicInterpolation(ix0, ix1, sy);

            return result;
        }

        private static double CubicInterpolation(double a0, double a1, double w)
        {
            return (a1 - a0) * (3d - w * 2d) * w * w + a0;
        }
    }
}