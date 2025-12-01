using Microsoft.Xna.Framework;

namespace Mmc.MonoGame.Curves._2D
{
    /// <summary>
    /// Combines arbitrary amount of curves into one.
    /// </summary>
    public class CompoundCurve2D : Curve2D
    {
        /// <summary>
        /// Curves defining the compound curve.
        /// </summary>
        public List<Curve2D> Curves { get; private init; } = [];

        public override bool IsSmooth => CheckIfSmooth();

        /// <summary>
        /// Construct new instance of CompoundCurve2D.
        /// </summary>
        /// <param name="curves">Initial curves to construct compoud curve.</param>
        public CompoundCurve2D(params Curve2D[] curves)
        {
            Curves.AddRange(curves);
        }

        public override Vector2 GetPoint(float t)
        {
            if (Curves.Count == 0) return Vector2.Zero;

            (int curveIndex, float localT) = ConvertGlobalToLocalT(t);

            Curve2D targetCurve = Curves[curveIndex];

            return targetCurve.GetPoint(localT);
        }

        public override Vector2 GetTangent(float t)
        {
            if (Curves.Count == 0) return Vector2.Zero;

            (int curveIndex, float localT) = ConvertGlobalToLocalT(t);

            Curve2D targetCurve = Curves[curveIndex];

            return targetCurve.GetTangent(localT);
        }

        protected virtual (int curveIndex, float curveT) ConvertGlobalToLocalT(float t)
        {
            int curveIndex = Math.Clamp((int)(t * Curves.Count), 0, Curves.Count - 1);
            float localT = t * Curves.Count - curveIndex;

            return (curveIndex, localT);
        }

        /// <summary>
        /// Check if continuity and smooth derivative transitions.
        /// </summary>
        /// <returns>Whether or not the curve is smooth.</returns>
        protected virtual bool CheckIfSmooth()
        {
            const float TangentDiffMargin = .1f;
            const float PositionDiffMargin = .01f;

            if (Curves.Count == 0) return false;

            for (int i = 0; i < Curves.Count - 1; i++)
            {
                var tangentOfEnd = Vector2.Normalize(Curves[i].GetTangent(1));
                var tangentOfEndScalar = tangentOfEnd.Y / tangentOfEnd.X;
                var tangentOfNextStart = Vector2.Normalize(Curves[i + 1].GetTangent(0));
                var tangentOfNextStartScalar = tangentOfNextStart.Y / tangentOfNextStart.X;

                bool curveIsSmooth = Curves[i].IsSmooth;
                bool smoothSlopeTransition = MathF.Abs(tangentOfEndScalar - tangentOfNextStartScalar) <= TangentDiffMargin;
                bool smoothPositionTransition = Vector2.Distance(Curves[i].GetPoint(1), Curves[i + 1].GetPoint(0)) <= PositionDiffMargin;
                if (!curveIsSmooth || !smoothSlopeTransition || !smoothPositionTransition)
                    return false;
            }

            return Curves[^1].IsSmooth;
        }
    }
}
