using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Rendering;

namespace Mmc.MonoGame.UI.Models.Brushes
{
    public interface IBrush
    {
        void Draw(UIElement host, RenderContext renderContext, Rectangle destinationRectangle);
    }
}
