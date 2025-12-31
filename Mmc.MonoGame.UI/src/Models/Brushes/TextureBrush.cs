using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Primitives;

namespace Mmc.MonoGame.UI.Models.Brushes
{
    public class TextureBrush : SolidBrush
    {
        public Texture2D? Texture { get; set; }

        /// <summary>
        /// Texture atlas coordinates if the Texture is not just a single image (leave null otherwise)
        /// </summary>
        public Rectangle? SourceRectangle { get; set; }

        /// <summary>
        /// Define how texture should be placed in its designated rectangle
        /// </summary>
        public TextureMode TextureMode { get; set; } = TextureMode.Stretch;

        /// <summary>
        /// If texture is null, should it default to the base color
        /// </summary>
        public bool DefaultToSolidColorIfNoTexture { get; set; } = true;

        /// <summary>
        /// Defines how big the edges are for TextureMode.NineSlice
        /// </summary>
        public int EdgeSize { get; set; } = 1;

        public override void Draw(UIElement host, SpriteBatch spriteBatch, Rectangle destinationRectangle)
        {
            if (Texture == null)
            {
                if (DefaultToSolidColorIfNoTexture)
                    base.Draw(host, spriteBatch, destinationRectangle);

                return;
            }

            Rectangle trueSourceRectangle = SourceRectangle ?? new Rectangle(0, 0, Texture.Width, Texture.Height);

            switch (TextureMode)
            {
                case TextureMode.Stretch:
                    spriteBatch.Draw(Texture, destinationRectangle, trueSourceRectangle, Color);
                    break;

                case TextureMode.Center:
                    Vector2 position = new Vector2(
                        destinationRectangle.X + (destinationRectangle.Width - trueSourceRectangle.Width) / 2f,
                        destinationRectangle.Y + (destinationRectangle.Height - trueSourceRectangle.Height) / 2f
                    );

                    spriteBatch.Draw(Texture, position, trueSourceRectangle, Color);
                    break;

                case TextureMode.Tile:
                    DrawTiled(spriteBatch, destinationRectangle, trueSourceRectangle);
                    break;

                case TextureMode.NineSlice:
                    DrawNineSlice(spriteBatch, destinationRectangle, trueSourceRectangle);
                    break;
            }
        }

        private void DrawTiled(SpriteBatch spriteBatch, Rectangle destinationRectangle, Rectangle sourceRectangle)
        {
            int tileWidth = sourceRectangle.Width;
            int tileHeight = sourceRectangle.Height;

            for (int y = destinationRectangle.Y; y < destinationRectangle.Bottom; y += tileHeight)
            {
                for (int x = destinationRectangle.X; x < destinationRectangle.Right; x += tileWidth)
                {
                    int widthToDraw = Math.Min(tileWidth, destinationRectangle.Right - x);
                    int heightToDraw = Math.Min(tileHeight, destinationRectangle.Bottom - y);

                    Rectangle partialSrc = new Rectangle(sourceRectangle.X, sourceRectangle.Y, widthToDraw, heightToDraw);

                    Rectangle partialDest = new Rectangle(x, y, widthToDraw, heightToDraw);

                    spriteBatch.Draw(Texture, partialDest, partialSrc, Color);
                }
            }
        }

        private void DrawNineSlice(SpriteBatch spriteBatch, Rectangle destinationRectangle, Rectangle sourceRectangle)
        {
            int s = EdgeSize;

            // size of the source middle piece
            int middleSourceWidth = sourceRectangle.Width - 2 * s;
            int middleSourceHeight = sourceRectangle.Height - 2 * s;

            // size of the destination middle piece
            int middleDestinationWidth = destinationRectangle.Width - 2 * s;
            int middleDestinationHeight = destinationRectangle.Height - 2 * s;

            // top left
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.X, sourceRectangle.Y, s, s),
                new Rectangle(destinationRectangle.X, destinationRectangle.Y, s, s));

            // top middle
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.X + s, sourceRectangle.Y, middleSourceWidth, s),
                new Rectangle(destinationRectangle.X + s, destinationRectangle.Y, middleDestinationWidth, s));

            // top right
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.Right - s, sourceRectangle.Y, s, s),
                new Rectangle(destinationRectangle.Right - s, destinationRectangle.Y, s, s));

            // middle left
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.X, sourceRectangle.Y + s, s, middleSourceHeight),
                new Rectangle(destinationRectangle.X, destinationRectangle.Y + s, s, middleDestinationHeight));

            // center
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.X + s, sourceRectangle.Y + s, middleSourceWidth, middleSourceHeight),
                new Rectangle(destinationRectangle.X + s, destinationRectangle.Y + s, middleDestinationWidth, middleDestinationHeight));

            // middle right
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.Right - s, sourceRectangle.Y + s, s, middleSourceHeight),
                new Rectangle(destinationRectangle.Right - s, destinationRectangle.Y + s, s, middleDestinationHeight));

            // bottom left
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.X, sourceRectangle.Bottom - s, s, s),
                new Rectangle(destinationRectangle.X, destinationRectangle.Bottom - s, s, s));

            // bottom middle
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.X + s, sourceRectangle.Bottom - s, middleSourceWidth, s),
                new Rectangle(destinationRectangle.X + s, destinationRectangle.Bottom - s, middleDestinationWidth, s));

            // bottom right
            DrawNineSliceSegment(spriteBatch,
                new Rectangle(sourceRectangle.Right - s, sourceRectangle.Bottom - s, s, s),
                new Rectangle(destinationRectangle.Right - s, destinationRectangle.Bottom - s, s, s));
        }

        private void DrawNineSliceSegment(SpriteBatch spriteBatch, Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            if (destinationRectangle.Width > 0 && destinationRectangle.Height > 0 && sourceRectangle.Width > 0 && sourceRectangle.Height > 0)
            {
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color);
            }
        }
    }
}
