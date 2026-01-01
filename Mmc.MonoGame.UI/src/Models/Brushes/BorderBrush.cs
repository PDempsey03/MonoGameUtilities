using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mmc.MonoGame.UI.Base;

namespace Mmc.MonoGame.UI.Models.Brushes
{
    public class BorderBrush : SolidBrush
    {
        public override void Draw(UIElement host, SpriteBatch spriteBatch, Rectangle destinationRectangle)
        {
            var borderThickness = host.Border;

            Vector2 topLeft = new Vector2(destinationRectangle.Left, destinationRectangle.Top);
            Vector2 topRight = new Vector2(destinationRectangle.Right, destinationRectangle.Top);
            Vector2 bottomRight = new Vector2(destinationRectangle.Right, destinationRectangle.Bottom);
            Vector2 bottomLeft = new Vector2(destinationRectangle.Left, destinationRectangle.Bottom);

            // NOTE: its important to loop clockwise here to keep the thickness facing the right direction (inward)

            // top line 
            Drawer.DrawLine(spriteBatch, topLeft, topRight, Color, borderThickness.Top);

            // right line
            Drawer.DrawLine(spriteBatch, topRight, bottomRight, Color, borderThickness.Right);

            // bottom line
            Drawer.DrawLine(spriteBatch, bottomRight, bottomLeft, Color, borderThickness.Bottom);

            // left line
            Drawer.DrawLine(spriteBatch, bottomLeft, topLeft, Color, borderThickness.Left);
        }
    }
}
