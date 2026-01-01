using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Primitives;
using Mmc.MonoGame.UI.Models.Text;
using Mmc.MonoGame.UI.Rendering;
using Mmc.MonoGame.UI.Systems.Text;

namespace Mmc.MonoGame.UI.UIElements
{
    public class Label : UIElement
    {
        private List<MeasuredWord> _words = [];
        private bool _wrap = false;
        private Color _color = Color.White;
        private TextHorizontalAlignment _textHorizontalAlignment = TextHorizontalAlignment.Left;
        private TextVerticalAlignment _textVerticalAlignment = TextVerticalAlignment.Top;

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

        public TextHorizontalAlignment TextHorizontalAlignment
        {
            get => _textHorizontalAlignment;
            set
            {
                if (_textHorizontalAlignment != value)
                {
                    _textHorizontalAlignment = value;
                    MarkDirty();
                }
            }
        }

        public TextVerticalAlignment TextVerticalAlignment
        {
            get => _textVerticalAlignment;
            set
            {
                if (_textVerticalAlignment != value)
                {
                    _textVerticalAlignment = value;
                    MarkDirty();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void InternalDraw(RenderContext renderContext)
        {
            base.InternalDraw(renderContext);

            if (FontFamily == null || string.IsNullOrEmpty(Text)) return;

            Vector2 textPosition = ContentBounds.Location.ToVector2(); // top left of the content area

            var spriteBatch = renderContext.SpriteBatch;

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

            List<MeasuredWord> words = TextLayoutProcessor.ProcessText(Text, FontFamily, TextColor);

            float totalWidth = 0;
            float maxHeight = 0;

            float marginsX = Margin.Horizontal;
            float bordersX = Border.Horizontal;
            float paddingX = Padding.Horizontal;

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

            float finalWidth = Size.X > 0 ? Size.X : marginsX + bordersX + paddingX + totalWidth;
            float finalHeight = Size.Y > 0 ? Size.Y : Margin.Vertical + Border.Vertical + Padding.Vertical + maxHeight;

            DesiredSize = new Vector2(finalWidth, finalHeight);
        }

        public override void Arrange(Rectangle finalRect)
        {
            base.Arrange(finalRect);

            // nothing to be done for top left
            if (TextHorizontalAlignment == TextHorizontalAlignment.Left && TextVerticalAlignment == TextVerticalAlignment.Top) return;

            TextLayoutProcessor.ApplyTextAlignment(_words, _contentBounds, TextHorizontalAlignment, TextVerticalAlignment);
        }
    }
}
