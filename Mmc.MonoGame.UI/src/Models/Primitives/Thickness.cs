namespace Mmc.MonoGame.UI.Models.Primitives
{
    public struct Thickness : IEquatable<Thickness>
    {
        public static readonly Thickness None = new Thickness(0);

        public float Left, Top, Right, Bottom;

        public readonly float Horizontal => Left + Right;

        public readonly float Vertical => Top + Bottom;

        public Thickness(float all)
        {
            Left = Top = Right = Bottom = all;
        }

        public Thickness(float horizontal, float vertical)
        {
            Left = Right = horizontal;
            Top = Bottom = vertical;
        }

        public Thickness(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Thickness() : this(0) { }

        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(a.Left + b.Left, a.Top + b.Top, a.Right + b.Right, a.Bottom + b.Bottom);
        }

        public static Thickness operator -(Thickness a, Thickness b)
        {
            return new Thickness(a.Left - b.Left, a.Top - b.Top, a.Right - b.Right, a.Bottom - b.Bottom);
        }

        public static bool operator ==(Thickness left, Thickness right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Thickness left, Thickness right)
        {
            return !(left == right);
        }

        public readonly bool Equals(Thickness other)
        {
            return Left == other.Left &&
                   Top == other.Top &&
                   Right == other.Right &&
                   Bottom == other.Bottom;
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Thickness other && Equals(other);
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Left, Top, Right, Bottom);
        }
    }
}
