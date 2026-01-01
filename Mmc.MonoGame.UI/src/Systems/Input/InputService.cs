using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mmc.MonoGame.UI.Models.Events;
using Mmc.MonoGame.UI.Models.Primitives;

namespace Mmc.MonoGame.UI.Systems.Input
{
    public class InputService
    {
        public MouseState PreviousMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public Vector2 PreviousMousePosition => PreviousMouseState.Position.ToVector2();
        public Vector2 CurrentMousePosition => CurrentMouseState.Position.ToVector2();

        public bool WasLeftMouseButtonPressed => PreviousMouseState.LeftButton == ButtonState.Pressed;
        public bool IsLeftMouseButtonPressed => CurrentMouseState.LeftButton == ButtonState.Pressed;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonPressed;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonReleased;
        public event EventHandler<MouseButtonEventArgs>? MouseButtonHeld;

        public bool WasRightMouseButtonPressed => PreviousMouseState.RightButton == ButtonState.Pressed;
        public bool IsRightMouseButtonPressed => CurrentMouseState.RightButton == ButtonState.Pressed;

        public bool WasMiddleMouseButtonPressed => PreviousMouseState.MiddleButton == ButtonState.Pressed;
        public bool IsMiddleMouseButtonPressed => CurrentMouseState.MiddleButton == ButtonState.Pressed;

        public bool WasXMouseButton1Pressed => PreviousMouseState.XButton1 == ButtonState.Pressed;
        public bool IsXMouseButton1Pressed => CurrentMouseState.XButton1 == ButtonState.Pressed;

        public bool WasXMouseButton2Pressed => PreviousMouseState.XButton2 == ButtonState.Pressed;
        public bool IsXMouseButton2Pressed => CurrentMouseState.XButton2 == ButtonState.Pressed;

        public int PreviousMouseWheel => PreviousMouseState.ScrollWheelValue;
        public int CurrentMouseWheel => CurrentMouseState.ScrollWheelValue;
        public event EventHandler<EventArgs>? MouseScrolled;

        public InputService()
        {
            CurrentMouseState = Mouse.GetState();
        }

        public void UpdateState()
        {
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        public void UpdateEvents()
        {
            CheckLeftButton();
            CheckRightButton();
            CheckMiddleButton();
            CheckXButton1();
            CheckXButton2();
            CheckScrollWheel();
        }

        private void CheckLeftButton()
        {
            bool wasLeftButtonPressed = WasLeftMouseButtonPressed;
            bool isLeftButtonPressed = IsLeftMouseButtonPressed;

            if (isLeftButtonPressed && !wasLeftButtonPressed)
                MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(MouseButton.Left, CurrentMousePosition));
            else if (!isLeftButtonPressed && wasLeftButtonPressed)
                MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(MouseButton.Left, CurrentMousePosition));
            else if (isLeftButtonPressed && wasLeftButtonPressed)
                MouseButtonHeld?.Invoke(this, new MouseButtonEventArgs(MouseButton.Left, CurrentMousePosition));
        }

        private void CheckRightButton()
        {
            bool wasRightButtonPressed = WasRightMouseButtonPressed;
            bool isRightButtonPressed = IsRightMouseButtonPressed;

            if (isRightButtonPressed && !wasRightButtonPressed)
                MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(MouseButton.Right, CurrentMousePosition));
            else if (!isRightButtonPressed && wasRightButtonPressed)
                MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(MouseButton.Right, CurrentMousePosition));
            else if (isRightButtonPressed && wasRightButtonPressed)
                MouseButtonHeld?.Invoke(this, new MouseButtonEventArgs(MouseButton.Right, CurrentMousePosition));
        }

        private void CheckMiddleButton()
        {
            bool wasMiddleButtonPressed = WasMiddleMouseButtonPressed;
            bool isMiddleButtonPressed = IsMiddleMouseButtonPressed;

            if (isMiddleButtonPressed && !wasMiddleButtonPressed)
                MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(MouseButton.Middle, CurrentMousePosition));
            else if (!isMiddleButtonPressed && wasMiddleButtonPressed)
                MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(MouseButton.Middle, CurrentMousePosition));
            else if (isMiddleButtonPressed && wasMiddleButtonPressed)
                MouseButtonHeld?.Invoke(this, new MouseButtonEventArgs(MouseButton.Middle, CurrentMousePosition));
        }

        private void CheckXButton1()
        {
            bool wasXButton1Pressed = WasXMouseButton1Pressed;
            bool isXButton1Presssed = IsXMouseButton1Pressed;

            if (isXButton1Presssed && !wasXButton1Pressed)
                MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(MouseButton.XButton1, CurrentMousePosition));
            else if (!isXButton1Presssed && wasXButton1Pressed)
                MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(MouseButton.XButton1, CurrentMousePosition));
            else if (isXButton1Presssed && wasXButton1Pressed)
                MouseButtonHeld?.Invoke(this, new MouseButtonEventArgs(MouseButton.XButton1, CurrentMousePosition));
        }

        private void CheckXButton2()
        {
            bool wasXButton2Pressed = WasXMouseButton2Pressed;
            bool isXButton2Presssed = IsXMouseButton2Pressed;

            if (isXButton2Presssed && !wasXButton2Pressed)
                MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(MouseButton.XButton2, CurrentMousePosition));
            else if (!isXButton2Presssed && wasXButton2Pressed)
                MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(MouseButton.XButton2, CurrentMousePosition));
            else if (isXButton2Presssed && wasXButton2Pressed)
                MouseButtonHeld?.Invoke(this, new MouseButtonEventArgs(MouseButton.XButton2, CurrentMousePosition));
        }

        private void CheckScrollWheel()
        {
            int delta = CurrentMouseWheel - PreviousMouseWheel;

            if (delta != 0)
            {
                MouseScrolled?.Invoke(this, new ScrollWheelEventArgs(delta));
            }
        }
    }
}
