using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Models.Primitives;

namespace Mmc.MonoGame.UI.Base
{
    public static class UIElementExtensions
    {
        public static Rectangle CalculateChildRectangle(this UIElement child, Rectangle contentBounds)
        {
            // determine width which for stretch is the whole rectangle's width, otherwise, clamp its desired width to rectangle width
            float width = child.HorizontalAlignment switch
            {
                HorizontalAlignment.Stretch => contentBounds.Width,
                _ => Math.Min(child.DesiredSize.X, contentBounds.Width)
            };

            // same for the height as the width
            float height = child.VerticalAlignment switch
            {
                VerticalAlignment.Stretch => contentBounds.Height,
                _ => Math.Min(child.DesiredSize.Y, contentBounds.Height)
            };

            // determine x based on the horizontal alignment type
            float x = child.HorizontalAlignment switch
            {
                HorizontalAlignment.Center => contentBounds.X + (contentBounds.Width - width) / 2f,
                HorizontalAlignment.Right => contentBounds.X + contentBounds.Width - width,
                _ => contentBounds.X // left and stretch both start at the far left
            };

            // determine y based on the vertical alignment type
            float y = child.VerticalAlignment switch
            {
                VerticalAlignment.Center => contentBounds.Y + (contentBounds.Height - height) / 2f,
                VerticalAlignment.Bottom => contentBounds.Y + contentBounds.Height - height,
                _ => contentBounds.Y // top and stretch both start at the top
            };

            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
    }
}
