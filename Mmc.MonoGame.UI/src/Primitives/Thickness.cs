namespace Mmc.MonoGame.UI.Primitives
{
    public struct Thickness
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
    }
}
