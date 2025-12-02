namespace Mmc.MonoGame.Input.Conditions
{
    public sealed class AnyCondition : Condition
    {
        private readonly Condition[] _conditions;

        public AnyCondition(params Condition[] conditions) : base(InputType.None)
        {
            _conditions = conditions;
        }

        public override bool IsConditionMet()
        {
            return _conditions.Any(condition => condition.IsConditionMet());
        }
    }
}
