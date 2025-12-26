using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Primitives.Brushes;

namespace Mmc.MonoGame.UI.UIElements
{
    public class Panel : ContainerElement
    {
        // brushes
        public IBrush? BorderBrush { get; set; }
        public IBrush? BackgroundBrush { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BackgroundBrush?.Draw(this, spriteBatch, ContentBounds);
            BorderBrush?.Draw(this, spriteBatch, GlobalBounds);

            base.Draw(spriteBatch);
        }
    }
}
