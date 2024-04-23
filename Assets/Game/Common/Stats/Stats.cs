namespace Game.Common.Stats
{
    public class Stats : IStats<int>
    {
        private readonly GenericStats<int> _intStats;

        public Stats()
        {
            _intStats = new GenericStats<int>();
        }
    
        public bool TryGet(string id, out ITypeStat<int> stat)
        {
            return _intStats.TryGet(id, out stat);
        }

        public ITypeStat<int> Get(string id)
        {
            return _intStats.Get(id);
        }

        public IStats<int> Add(ITypeStat<int> stat)
        {
            _intStats.Add(stat);
            return this;
        }
    }
}