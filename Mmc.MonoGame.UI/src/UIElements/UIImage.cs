using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Primitives;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.UIElements
{
    public class UIImage : UIElement
    {
        private Texture2D? _texture;

        public Texture2D? Texture
        {
            get => _texture;
            set
            {
                if (_texture != value)
                {
                    _texture = value;
                    MarkDirty();
                }
            }
        }

        public Color Tint { get; set; } = Color.White;

        public ImageStretchMode StretchMode { get; set; } = ImageStretchMode.None;

        public override void Update(GameTime gameTime)
        {

        }

        public override void InternalDrawContent(RenderContext renderContext)
        {
            base.InternalDrawContent(renderContext);

            if (_texture == null) return;

            renderContext.SpriteBatch.Draw(_texture, CalculateImageDestinationRectangle(), Tint);
        }

        protected virtual Rectangle CalculateImageDestinationRectangle()
        {
            if (_texture == null) return Rectangle.Empty;

            Rectangle destinationRectangle = _contentBounds;

            // do nothing for fill
            if (StretchMode != ImageStretchMode.Fill)
            {
                float textureWidth = _texture.Width;
                float textureHeight = _texture.Height;
                float targetWidth = _contentBounds.Width;
                float targetHeight = _contentBounds.Height;

                float textureAspectRatio = textureWidth / textureHeight;
                float targetAspectRatio = targetWidth / targetHeight;

                int finalWidth = (int)targetWidth;
                int finalHeight = (int)targetHeight;

                switch (StretchMode)
                {
                    case ImageStretchMode.None:
                        finalWidth = (int)textureWidth;
                        finalHeight = (int)textureHeight;
                        break;

                    case ImageStretchMode.Uniform:
                        if (targetAspectRatio > textureAspectRatio)
                            finalWidth = (int)(targetHeight * textureAspectRatio);
                        else
                            finalHeight = (int)(targetWidth / textureAspectRatio);
                        break;

                    case ImageStretchMode.UniformFill:
                        if (targetAspectRatio > textureAspectRatio)
                            finalHeight = (int)(targetWidth / textureAspectRatio);
                        else
                            finalWidth = (int)(targetHeight * textureAspectRatio);
                        break;
                }

                // center the resulting rectangle within content bounds
                int x = _contentBounds.X + (_contentBounds.Width - finalWidth) / 2;
                int y = _contentBounds.Y + (_contentBounds.Height - finalHeight) / 2;
                destinationRectangle = new Rectangle(x, y, finalWidth, finalHeight);
            }

            return destinationRectangle;
        }
    }
}
