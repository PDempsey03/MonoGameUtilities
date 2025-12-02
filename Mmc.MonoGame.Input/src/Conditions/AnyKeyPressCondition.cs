namespace Mmc.MonoGame.Input.Conditions
{
    public class AnyKeyPressCondition : Condition
    {
        public AnyKeyPressCondition() : base(InputType.None)
        {

        }

        public override bool IsConditionMet()
        {
            return InputHelper.IsAnyKeyDown();
        }
    }
}
