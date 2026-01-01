using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Models.Primitives;

namespace Mmc.MonoGame.UI.Models.Events
{
    public class MouseButtonEventArgs : EventArgs
    {
        public MouseButton MouseButton { get; init; }
        public Vector2 MousePosition { get; init; }
        public bool Handled { get; set; } = false;

        public MouseButtonEventArgs(MouseButton mouseButton, Vector2 mousePosition)
        {
            MouseButton = mouseButton;
            MousePosition = mousePosition;
        }
    }
}
