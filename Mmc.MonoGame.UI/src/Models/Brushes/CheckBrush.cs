using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.Models.Brushes
{
    public class CheckBrush : SolidBrush
    {
        public int Thickness { get; set; } = 1;

        public override void Draw(UIElement host, RenderContext renderContext, Rectangle destinationRectangle)
        {
            int thirdWidth = destinationRectangle.Width / 3;
            int twoThirdWidth = 2 * thirdWidth;

            float intersectionX = destinationRectangle.X + thirdWidth;
            float intersectionY = destinationRectangle.Bottom + (twoThirdWidth - destinationRectangle.Height) / 2;

            Vector2 checkMarkIntersection = new Vector2(intersectionX, intersectionY);

            Vector2 leftStart = checkMarkIntersection - new Vector2(thirdWidth);
            Vector2 rightStart = checkMarkIntersection + new Vector2(twoThirdWidth, -twoThirdWidth);

            renderContext.DrawLine(checkMarkIntersection, leftStart, Color, Thickness);
            renderContext.DrawLine(rightStart, checkMarkIntersection, Color, Thickness);
        }
    }
}
