namespace Mmc.MonoGame.Input.Conditions
{
    public abstract class Condition
    {
        protected InputType _inputType;

        public Condition(InputType inputType)
        {
            _inputType = inputType;
        }

        public Condition()
        {
            _inputType = InputType.None;
        }

        public abstract bool IsConditionMet();
    }

    public enum InputType
    {
        Pressed,
        Held,
        Released,
        None, // used for cases of compound conditions
    }
}
