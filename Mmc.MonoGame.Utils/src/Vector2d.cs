using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace Mmc.MonoGame.Utils
{
    public struct Vector2d
    {
        public static readonly Vector2d Zero = new Vector2d(0.0, 0.0);
        public static readonly Vector2d One = new Vector2d(1.0, 1.0);
        public static readonly Vector2d UnitX = new Vector2d(1.0, 0.0);
        public static readonly Vector2d UnitY = new Vector2d(0.0, 1.0);

        public double X;
        public double Y;

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2d(double d) : this(d, d) { }

        public Vector2d() : this(0, 0) { }

        public readonly double Length => Math.Sqrt(LengthSquared);

        public readonly double LengthSquared => X * X + Y * Y;

        public Vector2d Normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                double len = Length;
                return len == 0 ? new Vector2d(0, 0) : new Vector2d(X / len, Y / len);
            }
        }

        public static Vector2d operator +(Vector2d a, Vector2d b) => new Vector2d(a.X + b.X, a.Y + b.Y);
        public static Vector2d operator -(Vector2d a, Vector2d b) => new Vector2d(a.X - b.X, a.Y - b.Y);
        public static Vector2d operator -(Vector2d a) => new Vector2d(-a.X, -a.Y);
        public static Vector2d operator *(Vector2d a, double s) => new Vector2d(a.X * s, a.Y * s);
        public static Vector2d operator /(Vector2d a, double s) => new Vector2d(a.X / s, a.Y / s);

        public static Vector2d operator *(double s, Vector2d a) => new Vector2d(a.X * s, a.Y * s);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(Vector2d a, Vector2d b) => a.X * b.X + a.Y * b.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector2d a, Vector2d b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceSquared(Vector2d a, Vector2d b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d Lerp(Vector2d a, Vector2d b, double t) => new Vector2d(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);

        public readonly Vector2 ToVector2() => new Vector2((float)X, (float)Y);
    }
}
