using Microsoft.Xna.Framework.Input;

namespace Mmc.MonoGame.Input.Conditions
{
    public class KeyboardCondition : Condition
    {
        protected Keys key;

        protected virtual Keys Key => key;

        public KeyboardCondition(Keys key, InputType inputType) : base(inputType)
        {
            this.key = key;
        }

        public override bool IsConditionMet()
        {
            return _inputType switch
            {
                InputType.Pressed => InputHelper.WasKeyUp(Key) && InputHelper.IsKeyDown(Key),
                InputType.Released => InputHelper.WasKeyDown(Key) && InputHelper.IsKeyUp(Key),
                InputType.Held => InputHelper.WasKeyDown(Key) && InputHelper.IsKeyDown(Key),
                _ => false
            };
        }
    }
}
