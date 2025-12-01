using Microsoft.Xna.Framework;
using Mmc.MonoGame.Noise;

namespace Mmc.MonoGame.Curves._2D.Noise
{
    /// <summary>
    /// Curve which applies noise to each point
    /// </summary>
    public class NoiseCurve2D : Curve2D
    {
        /// <summary>
        /// Curve to apply noise to.
        /// </summary>
        public Curve2D ParentCurve { get; set; }

        /// <summary>
        /// Noise to apply to the curve.
        /// </summary>
        public INoise Noise { get; set; }

        /// <summary>
        /// Scaling factor applied to the noise.
        /// </summary>
        public float Scale { get; set; }

        public override bool IsSmooth => ParentCurve.IsSmooth; // TODO: may need to adjust this as white noise probably wouldnt give a smooth curve even if the original curve is

        /// <summary>
        /// Construct instance of NoiseCurve2D.
        /// </summary>
        /// <param name="parentCurve">Curve which applies noise at each point</param>
        /// <param name="noise">Noise to apply to the curve.</param>
        /// <param name="scale">Scaling factor applied to the noise.</param>
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
