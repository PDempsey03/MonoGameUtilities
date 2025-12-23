using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;

namespace Mmc.MonoGame.UI.Primitives.Brushes
{
    public class SolidBrush : IBrush
    {
        public Color Color { get; set; } = Color.White;

        public virtual void Draw(UIElement host, SpriteBatch spriteBatch, Rectangle destinationRectangle)
        {
            Drawer.FillRectangle(spriteBatch, destinationRectangle, Color);
        }
    }
}
