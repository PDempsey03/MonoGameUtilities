namespace Mmc.MonoGame.Input.Conditions
{
    public class MouseScrollCondition : Condition
    {
        public override bool IsConditionMet()
        {
            return InputHelper.OldScrollWheelValue != InputHelper.NewScrollWheelValue;
        }
    }
}
