using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mmc.MonoGame.Input
{
    public static class InputHelper
    {
        private static KeyboardState oldKeyboardState;

        private static KeyboardState newKeyboardState;

        private static MouseState oldMouseState;

        private static MouseState newMouseState;

        public static bool WasKeyDown(Keys key)
        {
            return oldKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return newKeyboardState.IsKeyDown(key);
        }

        public static bool WasKeyUp(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return newKeyboardState.IsKeyUp(key);
        }

        public static bool IsAnyKeyDown()
        {
            return newKeyboardState.GetPressedKeyCount() > 0;
        }

        public static bool WasMouseButtonDown(MouseButton mouseButton)
        {
            return mouseButton switch
            {
                MouseButton.Left => oldMouseState.LeftButton == ButtonState.Pressed,
                MouseButton.Right => oldMouseState.RightButton == ButtonState.Pressed,
                MouseButton.Middle => oldMouseState.MiddleButton == ButtonState.Pressed,
                MouseButton.XButton1 => oldMouseState.XButton1 == ButtonState.Pressed,
                MouseButton.XButton2 => oldMouseState.XButton2 == ButtonState.Pressed,
                _ => false
            };
        }

        public static bool WasMouseButtonUp(MouseButton mouseButton)
        {
            return mouseButton switch
            {
                MouseButton.Left => oldMouseState.LeftButton == ButtonState.Released,
                MouseButton.Right => oldMouseState.RightButton == ButtonState.Released,
                MouseButton.Middle => oldMouseState.MiddleButton == ButtonState.Released,
                MouseButton.XButton1 => oldMouseState.XButton1 == ButtonState.Released,
                MouseButton.XButton2 => oldMouseState.XButton2 == ButtonState.Released,
                _ => false
            };
        }

        public static bool IsMouseButtonDown(MouseButton mouseButton)
        {
            return mouseButton switch
            {
                MouseButton.Left => newMouseState.LeftButton == ButtonState.Pressed,
                MouseButton.Right => newMouseState.RightButton == ButtonState.Pressed,
                MouseButton.Middle => newMouseState.MiddleButton == ButtonState.Pressed,
                MouseButton.XButton1 => newMouseState.XButton1 == ButtonState.Pressed,
                MouseButton.XButton2 => newMouseState.XButton2 == ButtonState.Pressed,
                _ => false
            };
        }

        public static bool IsMouseButtonUp(MouseButton mouseButton)
        {
            return mouseButton switch
            {
                MouseButton.Left => newMouseState.LeftButton == ButtonState.Released,
                MouseButton.Right => newMouseState.RightButton == ButtonState.Released,
                MouseButton.Middle => newMouseState.MiddleButton == ButtonState.Released,
                MouseButton.XButton1 => newMouseState.XButton1 == ButtonState.Released,
                MouseButton.XButton2 => newMouseState.XButton2 == ButtonState.Released,
                _ => false
            };
        }

        public static Point OldMousePosition => oldMouseState.Position;

        public static Point NewMousePosition => newMouseState.Position;

        public static int OldScrollWheelValue => oldMouseState.ScrollWheelValue;

        public static int NewScrollWheelValue => newMouseState.ScrollWheelValue;

        public static void UpdateState()
        {
            oldKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();

            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();
        }
    }

    public enum MouseButton
    {
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }
}
