using Mmc.MonoGame.Utils;

namespace Mmc.MonoGame.Noise
{
    public class OpenSimplexNoise : INoise
    {
        private const double EmpiricalScalingConstant = 70d / (1.41421356237d / 2d);

        private PseudoRandom PseudoRandom { get; init; }

        private double ZoomFactor { get; init; }

        public OpenSimplexNoise(int seed, double zoomFactor = 100)
        {
            PseudoRandom = new PseudoRandom(seed);
            ZoomFactor = zoomFactor;
        }

        public double GetValue(double x, double y)
        {
            x /= ZoomFactor;
            y /= ZoomFactor;

            // skewing factors for triangular space
            const double F2 = 0.366025403d; // (sqrt(3)-1)/2
            const double G2 = 0.211324865d; // (3-sqrt(3))/6

            // skew input space to determine which simplex cell we're in
            double s = (x + y) * F2;
            int i = IntegerFloor(x + s);
            int j = IntegerFloor(y + s);

            // unskewed lattice cell origin
            double t = (i + j) * G2;

            // corner 0 position in unskewed cartesian space
            double X0 = i - t;
            double Y0 = j - t;

            // distance to corner 0
            double x0 = x - X0;
            double y0 = y - Y0;

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
            double x1 = x0 - i1 + G2;
            double y1 = y0 - j1 + G2;
            double x2 = x0 - 1 + 2 * G2;
            double y2 = y0 - 1 + 2 * G2;

            // get the contribution of each corner on the triangle
            double n0 = CornerContribution(i, j, x0, y0);
            double n1 = CornerContribution(i + i1, j + j1, x1, y1);
            double n2 = CornerContribution(i + 1, j + 1, x2, y2);

            // Scale result to [-1,1]
            return Math.Clamp(EmpiricalScalingConstant * (n0 + n1 + n2), -1f, 1f);
        }

        private double CornerContribution(int i, int j, double x, double y)
        {
            // radius of influence is .5^2
            // calculates squared distance to corner
            double t = 0.5d - x * x - y * y;
            if (t < 0) return 0d;

            // get true distance 
            t *= t;

            // get the gradient at the corner
            double angle = PseudoRandom.GetRandomValueInRange(0, MathF.Tau, i, j);

            double xComponent = Math.Cos(angle);
            double yComponent = Math.Sin(angle);

            // return dot product with t^2 as a smoothness since interpolation isn't done anywhere else
            return t * t * (xComponent * x + yComponent * y);
        }

        private static int IntegerFloor(double x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }
    }
}
