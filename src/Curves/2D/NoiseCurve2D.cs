using Microsoft.Xna.Framework;
using Mmc.MonoGame.Utils.Noise;

namespace Mmc.MonoGame.Utils.Curves._2D.Noise
{
    public class NoiseCurve2D : Curve2D
    {
        public Curve2D ParentCurve { get; set; }

        public INoise Noise { get; set; }

        public float Scale { get; set; }

        public override bool IsSmooth => ParentCurve.IsSmooth; // TODO: may need to adjust this

        public NoiseCurve2D(Curve2D parentCurve, INoise noise, float scale = 1)
        {
            ParentCurve = parentCurve;
            Noise = noise;
            Scale = scale;
        }

        public override Vector2 GetPoint(float t)
        {
            if (ParentCurve == null) return Vector2.Zero;

            Vector2 originalPoint = ParentCurve.GetPoint(t);

            return originalPoint + Scale * Noise.GetValue(originalPoint.X, originalPoint.Y) * Vector2.Normalize(ParentCurve.GetNormal(t));
        }

        public override Vector2 GetTangent(float t)
        {
            if (ParentCurve == null) return Vector2.Zero;

            // resort to approximation as no closed form solution for noise based curves
            float delta = 0.0001f; // small step for approximation
            Vector2 p1 = GetPoint(t);
            Vector2 p2 = GetPoint(t + delta);
            return p2 - p1;
        }
    }
}
