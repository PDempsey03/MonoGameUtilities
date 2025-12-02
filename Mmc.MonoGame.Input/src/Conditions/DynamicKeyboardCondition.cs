using Microsoft.Xna.Framework.Input;

namespace Mmc.MonoGame.Input.Conditions
{
    public class DynamicKeyboardCondition : KeyboardCondition
    {
        protected readonly Func<Keys> _getKeyFunc;

        protected override Keys Key => _getKeyFunc();

        public DynamicKeyboardCondition(Func<Keys> getKeyFunc, InputType inputType) : base(Keys.None, inputType)
        {
            _getKeyFunc = getKeyFunc;
        }
    }
}
