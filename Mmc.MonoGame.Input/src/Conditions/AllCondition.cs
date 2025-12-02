namespace Mmc.MonoGame.Input.Conditions
{
    public sealed class AllCondition : Condition
    {
        private readonly Condition[] _conditions;

        public AllCondition(params Condition[] conditions) : base(InputType.None)
        {
            _conditions = conditions;
        }

        public override bool IsConditionMet()
        {
            return _conditions.All(condition => condition.IsConditionMet());
        }
    }
}
