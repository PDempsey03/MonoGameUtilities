using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Noise
{
    // Implementation based off https://www.youtube.com/watch?v=kCIaHqb60Cw
    public class PerlinNoise : INoise
    {
        private const float EmpiricalScalingConstant = 1 / .63f;

        private PseudoRandom PseudoRandom { get; init; }

        private int ZoomFactor { get; init; }

        /// <summary>
        /// Construct Instance of Perlin Noise
        /// </summary>
        /// <param name="seed">Defines the random seed used when generating the perlin noise</param>
        /// <param name="zoomFactor">Defines how zoomed in on the perlin noise the output will be. (100-400 is a good starting range)</param>
        public PerlinNoise(int seed, int zoomFactor = 100)
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
            float sx = Fade(x - x0);
            float sy = Fade(y - y0);

            // compute and interpolate top two corners
            float n0 = DotGridGradient(x0, y0, x, y);
            float n1 = DotGridGradient(x1, y0, x, y);
            float ix0 = MathHelper.Lerp(n0, n1, sx);

            // compute and interpolate bottom two corners
            n0 = DotGridGradient(x0, y1, x, y);
            n1 = DotGridGradient(x1, y1, x, y);
            float ix1 = MathHelper.Lerp(n0, n1, sx);

            float unscaledResult = MathHelper.Lerp(ix0, ix1, sy);

            // interpolate previously interpolated horizontal values vertically to get answer
            float result = MathHelper.Clamp(EmpiricalScalingConstant * unscaledResult, -1, 1);

            return result;
        }

        private float DotGridGradient(int ix, int iy, float x, float y)
        {
            // get gradient at the provided integer coordinates
            float angle = PseudoRandom.GetRandomValueInRange(0, MathF.Tau, ix, iy);
            Vector2 gradient = new Vector2(MathF.Cos(angle), MathF.Sin(angle));

            // compute distance vector
            float dx = x - ix;
            float dy = y - iy;

            // compute dot product [-sqrt(2), sqrt(2)]
            float dotProduct = (dx * gradient.X + dy * gradient.Y);

            return dotProduct;
        }

        private static float Fade(float t)
        {
            return ((6 * t - 15) * t + 10) * t * t * t;
        }
    }
}
