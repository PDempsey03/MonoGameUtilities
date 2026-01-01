using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.Models.Brushes
{
    public class SolidBrush : IBrush
    {
        public Color Color { get; set; } = Color.White;

        public virtual void Draw(UIElement host, RenderContext renderContext, Rectangle destinationRectangle)
        {
            Drawer.FillRectangle(renderContext.SpriteBatch, destinationRectangle, Color);
        }
    }
}
