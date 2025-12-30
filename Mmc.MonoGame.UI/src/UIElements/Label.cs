using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Primitives.Text;

namespace Mmc.MonoGame.UI.UIElements
{
    public class Label : UIElement
    {
        private List<MeasuredWord> _words = [];
        private bool _wrap = false;
        private Color _color = Color.White;

        protected string _text = string.Empty;

        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    MarkDirty();
                }
            }
        }

        public FontFamily? FontFamily { get; set; }

        public Color TextColor
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    MarkDirty();
                }
            }
        }

        public bool Wrap
        {
            get => _wrap;
            set
            {
                if (_wrap != value)
                {
                    _wrap = value;
                    MarkDirty();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (FontFamily == null || string.IsNullOrEmpty(Text)) return;

            Vector2 textPosition = ContentBounds.Location.ToVector2(); // top left of the content area

            foreach (var run in _words)
            {
                foreach (var segment in run.Segments)
                {
                    var segmentOffset = textPosition + segment.PositionOffset;

                    spriteBatch.DrawString(segment.Font, segment.Text, segmentOffset, segment.Color);

                    if (segment.IsUnderlined)
                    {
                        const int Thickness = 1;

                        Vector2 start = new Vector2(segmentOffset.X, segmentOffset.Y + segment.Size.Y - Thickness);

                        Vector2 end = start + new Vector2(segment.Size.X, 0);

                        Drawer.DrawLine(spriteBatch, start, end, Color.White, Thickness);
                    }
                }

            }
        }

        public override void Measure(Vector2 availableSize)
        {
            if (FontFamily == null || string.IsNullOrEmpty(Text))
            {
                DesiredSize = Vector2.Zero;
                return;
            }

            var parsedText = TextLayoutProcessor.ParseText(Text, FontFamily, TextColor);

            var words = TextLayoutProcessor.TokenizeTextRunSegments(parsedText);

            float totalWidth = 0;
            float maxHeight = 0;

            float marginsX = Margin.Left + Margin.Right;
            float bordersX = Border.Left + Border.Right;
            float paddingX = Padding.Left + Padding.Right;

            if (Wrap)
            {
                Vector2 wrappedSize = TextLayoutProcessor.WrapWords(words, (Size.X > 0 ? Size.X : availableSize.X) - (marginsX + bordersX + paddingX));
                totalWidth = wrappedSize.X;
                maxHeight = wrappedSize.Y;
            }
            else
            {
                foreach (var run in words)
                {
                    foreach (var segment in run.Segments)
                    {
                        totalWidth += segment.Size.X;
                        maxHeight = MathF.Max(maxHeight, segment.Size.Y);
                    }
                }
            }

            _words = words;

            float finalWidth;
            float finalHeight;
            if (Size.X > 0)
            {
                finalWidth = Size.X;
            }
            else
            {
                finalWidth = marginsX + bordersX + paddingX + totalWidth;
            }

            if (Size.Y > 0)
            {
                finalHeight = Size.Y;
            }
            else
            {
                float marginHeight = Margin.Top + Margin.Bottom;
                float internalHeight = Border.Top + Border.Bottom + Padding.Top + Padding.Bottom;
                finalHeight = marginHeight + internalHeight + maxHeight;
            }

            DesiredSize = new Vector2(finalWidth, finalHeight);
        }
    }
}
