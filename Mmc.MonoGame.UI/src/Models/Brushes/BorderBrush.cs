using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.Models.Brushes
{
    public class BorderBrush : SolidBrush
    {
        public override void Draw(UIElement host, RenderContext renderContext, Rectangle destinationRectangle)
        {
            var borderThickness = host.Border;

            Vector2 topLeft = new Vector2(destinationRectangle.Left, destinationRectangle.Top);
            Vector2 topRight = new Vector2(destinationRectangle.Right, destinationRectangle.Top);
            Vector2 bottomRight = new Vector2(destinationRectangle.Right, destinationRectangle.Bottom);
            Vector2 bottomLeft = new Vector2(destinationRectangle.Left, destinationRectangle.Bottom);

            // NOTE: its important to loop clockwise here to keep the thickness facing the right direction (inward)

            // top line 
            renderContext.DrawLine(topLeft, topRight, Color, borderThickness.Top);

            // right line
            renderContext.DrawLine(topRight, bottomRight, Color, borderThickness.Right);

            // bottom line
            renderContext.DrawLine(bottomRight, bottomLeft, Color, borderThickness.Bottom);

            // left line
            renderContext.DrawLine(bottomLeft, topLeft, Color, borderThickness.Left);
        }
    }
}
