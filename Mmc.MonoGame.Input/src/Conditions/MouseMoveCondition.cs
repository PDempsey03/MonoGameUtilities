namespace Mmc.MonoGame.Input.Conditions
{
    public class MouseMoveCondition : Condition
    {
        public MouseMoveCondition() : base(InputType.None)
        {

        }

        public override bool IsConditionMet()
        {
            return InputHelper.OldMousePosition != InputHelper.NewMousePosition;
        }
    }
}
