using System.Collections.Generic;

namespace Game.Common.Stats
{
    public class GenericStats<T> : IStats<T>
    {
        private readonly Dictionary<string, ITypeStat<T>> _stats;

        public GenericStats() : this(new Dictionary<string, ITypeStat<T>>())
        {
        }

        private GenericStats(Dictionary<string, ITypeStat<T>> stats)
        {
            _stats = stats;
        }

        public bool TryGet(string id, out ITypeStat<T> stat)
        {
            return _stats.TryGetValue(id, out stat);
        }
    
        public ITypeStat<T> Get(string id)
        {
            TryGet(id, out var stat);
            return stat;
        }

        public IStats<T> Add(ITypeStat<T> stat)
        {
            _stats[stat.Id()] = stat;
            return this;
        }
    }
}