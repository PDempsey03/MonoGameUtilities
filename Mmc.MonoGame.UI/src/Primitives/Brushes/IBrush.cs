using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;

namespace Mmc.MonoGame.UI.Primitives.Brushes
{
    public interface IBrush
    {
        void Draw(UIElement host, SpriteBatch spriteBatch, Rectangle destinationRectangle);
    }
}
