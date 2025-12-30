namespace Mmc.MonoGame.UI.Primitives.Text
{
    public sealed class MeasuredWord
    {
        public List<TextRunSegment> Segments { get; init; } = [];

        public float Width { get; set; }

        public float Height { get; set; }
    }
}
