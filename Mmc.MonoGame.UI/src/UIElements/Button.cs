using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Events;
using Mmc.MonoGame.UI.Models.Primitives;

namespace Mmc.MonoGame.UI.UIElements
{
    public class Button : ContentElement
    {
        private bool _isPressed = false;

        // events
        public event EventHandler<MouseButtonEventArgs>? Clicked;

        protected override void OnMouseButtonPressed(MouseButtonEventArgs args)
        {
            base.OnMouseButtonPressed(args);

            if (args.MouseButton == MouseButton.Left)
            {
                _isPressed = true;
            }

            args.Handled = true;
        }

        public override void OnMouseButtonReleased(MouseButtonEventArgs args)
        {
            base.OnMouseButtonReleased(args);

            if (args.MouseButton == MouseButton.Left && _isPressed)
            {
                _isPressed = false;

                if (IsMouseOver)
                {
                    Clicked?.Invoke(this, args);
                }
            }

            args.Handled = true;
        }

        public override void OnMouseButtonHeld(MouseButtonEventArgs args)
        {
            base.OnMouseButtonHeld(args);

            args.Handled = true;
        }
    }
}
