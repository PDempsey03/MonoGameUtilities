using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Utils.Curves._2D.Splines.Bezier
{
    public class GenericBezierCurve2D : Curve2D
    {
        public Vector2[] ControlPoints { get; set; }

        public override bool IsSmooth => true;

        public GenericBezierCurve2D(params Vector2[] args)
        {
            ControlPoints = args;
        }

        public override Vector2 GetPoint(float t)
        {
            if (ControlPoints == null || ControlPoints.Length == 0) return Vector2.Zero;

            int n = ControlPoints.Length - 1;

            Vector2 total = Vector2.Zero;

            for (int i = 0; i <= n; i++)
            {
                long nChooseI = NChooseK(n, i);

                float first = MathF.Pow(1 - t, n - i);

                float second = MathF.Pow(t, i);

                Vector2 localTotal = nChooseI * first * second * ControlPoints[i];

                total += localTotal;
            }

            return total;
        }

        public override Vector2 GetTangent(float t)
        {
            if (ControlPoints == null || ControlPoints.Length <= 1) return Vector2.Zero;

            int n = ControlPoints.Length - 1;

            Vector2 total = Vector2.Zero;

            for (int i = 0; i <= n; i++)
            {
                long nChooseI = NChooseK(n, i);

                float first = (i - n) * MathF.Pow(1 - t, MathF.Max(n - i - 1, 0)) * MathF.Pow(t, i);

                float second = i * MathF.Pow(1 - t, n - i) * MathF.Pow(t, MathF.Max(i - 1, 0));

                Vector2 localTotal = nChooseI * (first + second) * ControlPoints[i];

                total += localTotal;
            }

            return total;
        }

        private static long NChooseK(int n, int k)
        {
            if (k < 0 || k > n) return 0;
            if (k == 0 || k == n) return 1;

            if (k > n - k) k = n - k;

            long result = 1;

            for (int i = 1; i <= k; i++)
            {
                result = result * (n - (k - i)) / i;
            }

            return result;
        }
    }
}
