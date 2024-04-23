namespace Game.Common.Stats
{
    public class IntAdditionDecorator : StatsDecorator<int>
    {
        private readonly IStat<int> _stat;

        public IntAdditionDecorator(IStat<int> stat,IStat<int> child) : base(child)
        {
            _stat = stat;
        }

        public override int GetValue()
        {
            return _stat.GetValue() + Child.GetValue();
        }

        public override void Add(int value)
        {
            Child.Add(value);
        }
    }
}