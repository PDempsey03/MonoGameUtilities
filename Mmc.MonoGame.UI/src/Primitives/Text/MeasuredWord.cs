namespace Mmc.MonoGame.UI.Primitives.Text
{
    public sealed class MeasuredWord
    {
        public List<TextRunSegment> Segments { get; init; } = [];

        public float Width => Segments.Sum(s => s.Font.MeasureString(s.Text).X);

        public float Height => Segments.Max(s => s.Font.MeasureString(s.Text).Y);
    }
}
