namespace Mmc.MonoGame.Input.Conditions
{
    public class MouseButtonCondition : Condition
    {
        public MouseButton MouseButton { get; set; }

        public MouseButtonCondition(MouseButton mouseButton, InputType inputType) : base(inputType)
        {
            MouseButton = mouseButton;
        }

        public override bool IsConditionMet()
        {
            return _inputType switch
            {
                InputType.Pressed => InputHelper.WasMouseButtonUp(MouseButton) && InputHelper.IsMouseButtonDown(MouseButton),
                InputType.Released => InputHelper.WasMouseButtonDown(MouseButton) && InputHelper.IsMouseButtonUp(MouseButton),
                InputType.Held => InputHelper.WasMouseButtonDown(MouseButton) && InputHelper.IsMouseButtonDown(MouseButton),
                _ => false
            };
        }
    }
}
