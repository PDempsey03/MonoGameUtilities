using Microsoft.Xna.Framework;
using Mmc.MonoGame.Curves._2D;
using Mmc.MonoGame.Curves._2D.Polynomial;

namespace Mmc.MonoGame.Noise.Tests.Extensions
{
    public static class Curve2DExtensions
    {
        public static LinearCurve2D GetTangentLine(this Curve2D curve, float t, float tangentLineLength = 1)
        {
            Vector2 middle = curve.GetPoint(t);
            Vector2 tangent = curve.GetTangent(t);
            float slope = tangent.Y / tangent.X;

            LinearCurve2D tangentLine = new LinearCurve2D(slope, tangentLineLength, middle);

            return tangentLine;
        }

        public static LinearCurve2D GetNormalLine(this Curve2D curve, float t, float tangentLineLength = 1)
        {
            Vector2 middle = curve.GetPoint(t);
            Vector2 normal = curve.GetNormal(t);
            float slope = normal.Y / normal.X;

            LinearCurve2D tangentLine = new LinearCurve2D(slope, tangentLineLength, middle);

            return tangentLine;
        }
    }
}
