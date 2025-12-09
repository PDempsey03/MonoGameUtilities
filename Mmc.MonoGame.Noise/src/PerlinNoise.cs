using Mmc.MonoGame.Utils;
using Mmc.MonoGame.Utils.Extensions;

namespace Mmc.MonoGame.Noise
{
    // Implementation based off https://www.youtube.com/watch?v=kCIaHqb60Cw
    public class PerlinNoise : INoise
    {
        private const double EmpiricalScalingConstant = 1 / .63d;

        private PseudoRandom PseudoRandom { get; init; }

        private double ZoomFactor { get; init; }

        /// <summary>
        /// Construct Instance of Perlin Noise
        /// </summary>
        /// <param name="seed">Defines the random seed used when generating the perlin noise</param>
        /// <param name="zoomFactor">Defines how zoomed in on the perlin noise the output will be. (100-400 is a good starting range)</param>
        public PerlinNoise(int seed, double zoomFactor = 100)
        {
            PseudoRandom = new PseudoRandom(seed);
            ZoomFactor = zoomFactor;
        }

        public double GetValue(double x, double y)
        {
            x /= ZoomFactor;
            y /= ZoomFactor;

            // Grid cell coordinates
            int x0 = (int)Math.Floor(x);
            int y0 = (int)Math.Floor(y);
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            // Relative position inside cell
            double dx = x - x0;
            double dy = y - y0;

            // Interpolation weights
            double sx = Fade(dx);
            double sy = Fade(dy);

            // Dot products at the four corners
            double n00 = DotGridGradient(x0, y0, dx, dy);
            double n10 = DotGridGradient(x1, y0, dx - 1, dy);
            double n01 = DotGridGradient(x0, y1, dx, dy - 1);
            double n11 = DotGridGradient(x1, y1, dx - 1, dy - 1);

            // Interpolate
            double ix0 = DoubleExtensions.Lerp(n00, n10, sx);
            double ix1 = DoubleExtensions.Lerp(n01, n11, sx);
            double value = DoubleExtensions.Lerp(ix0, ix1, sy);

            // Scale to [-1,1]
            return Math.Clamp(EmpiricalScalingConstant * value, -1d, 1d);
        }

        private double DotGridGradient(int ix, int iy, double dx, double dy)
        {
            double randomAngle = PseudoRandom.GetRandomValueInRange(0, MathF.Tau, ix, iy);

            double xComponent = Math.Cos(randomAngle);
            double yComponent = Math.Sin(randomAngle);

            // Dot product
            return dx * xComponent + dy * yComponent;
        }

        private static double Fade(double t)
        {
            return ((6d * t - 15d) * t + 10d) * t * t * t;
        }
    }
}
