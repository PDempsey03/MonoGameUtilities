using Microsoft.Xna.Framework;
using Mmc.MonoGame.UI.Base;
using Mmc.MonoGame.UI.Models.Events;

namespace Mmc.MonoGame.UI.Systems.Input
{
    public class InteractionService
    {
        private readonly InputService _inputService;

        private readonly UIElement _root;

        private UIElement? _hoveredUIElement;

        public InteractionService(InputService inputService, UIElement root)
        {
            _inputService = inputService;

            _root = root;

            _inputService.MouseButtonPressed += HandleMouseButtonPressed;
            _inputService.MouseButtonReleased += HandleMouseButtonReleased;
            _inputService.MouseButtonHeld += HandleMouseButtonHeld;
        }

        public void Update()
        {
            UpdateHoverElement();
        }

        private void UpdateHoverElement()
        {
            Vector2 currentMousePosition = _inputService.CurrentMousePosition;

            UIElement? hitElement = InputHitDetector.FindElementAt(_root, currentMousePosition);

            if (hitElement != _hoveredUIElement)
            {
                _hoveredUIElement?.RaiseMouseLeave();
                _hoveredUIElement = hitElement;
                _hoveredUIElement?.RaiseMouseEnter();
            }
        }

        private void HandleMouseButtonPressed(object? sender, MouseButtonEventArgs args)
        {
            _hoveredUIElement?.RaiseMouseButtonPressed(args);
        }

        private void HandleMouseButtonReleased(object? sender, MouseButtonEventArgs args)
        {
            _hoveredUIElement?.RaiseMouseButtonReleased(args);
        }

        private void HandleMouseButtonHeld(object? sender, MouseButtonEventArgs args)
        {
            _hoveredUIElement?.RaiseMouseButtonHeld(args);
        }
    }
}
