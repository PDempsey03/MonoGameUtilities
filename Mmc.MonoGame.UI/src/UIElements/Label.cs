using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Primitives.Text;

namespace Mmc.MonoGame.UI.UIElements
{
    public class Label : UIElement
    {
        private TextRun[] _runs = [];

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

        public FontFamily FontFamily { get; set; }

        public Color TextColor { get; set; } = Color.White;

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (FontFamily == null || string.IsNullOrEmpty(Text)) return;

            Vector2 textPosition = ContentBounds.Location.ToVector2(); // top left of the content area

            foreach (var run in _runs)
            {
                spriteBatch.DrawString(run.Font, run.Text, textPosition, run.Color);

                if (run.IsUnderlined)
                {
                    const int Thickness = 1;

                    Vector2 start = new Vector2(textPosition.X, textPosition.Y + run.Size.Y - Thickness);

                    Vector2 end = start + new Vector2(run.Size.X, 0);

                    Drawer.DrawLine(spriteBatch, start, end, Color.White, Thickness);
                }

                textPosition.X += run.Size.X;
            }
        }

        public override void Measure(Vector2 availableSize)
        {
            if (FontFamily == null || string.IsNullOrEmpty(Text))
            {
                DesiredSize = Vector2.Zero;
                return;
            }

            _runs = TextParser.ParseText(Text, FontFamily, TextColor);

            float totalWidth = 0;
            float maxHeight = 0;

            foreach (var run in _runs)
            {
                totalWidth += run.Size.X;
                maxHeight = MathF.Max(maxHeight, run.Size.Y);
            }

            float finalWidth;
            float finalHeight;
            if (Size.X > 0)
            {
                finalWidth = Size.X;
            }
            else
            {
                float marginWidth = Margin.Left + Margin.Right;
                float internalWidth = Border.Left + Border.Right + Padding.Left + Padding.Right;
                finalWidth = marginWidth + internalWidth + totalWidth;
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
