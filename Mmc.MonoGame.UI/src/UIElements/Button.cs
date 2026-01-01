using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Events;

namespace Mmc.MonoGame.UI.UIElements
{
    public class Button : ContentElement
    {
        // events
        public event EventHandler<MouseButtonEventArgs>? MouseButtonPressed;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonReleased;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonHeld;

        protected override void OnMouseButtonPressed(MouseButtonEventArgs args)
        {
            MouseButtonPressed?.Invoke(this, args);
            args.Handled = true;

            base.OnMouseButtonPressed(args);
        }

        public override void OnMouseButtonReleased(MouseButtonEventArgs args)
        {
            MouseButtonReleased?.Invoke(this, args);
            args.Handled = true;

            base.OnMouseButtonReleased(args);
        }

        public override void OnMouseButtonHeld(MouseButtonEventArgs args)
        {
            MouseButtonHeld?.Invoke(this, args);
            args.Handled = true;

            base.OnMouseButtonHeld(args);
        }
    }
}
