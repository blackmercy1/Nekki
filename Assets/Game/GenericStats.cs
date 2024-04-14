using System.Collections.Generic;

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

    public IStats<T> Add(ITypeStat<T> stat)
    {
        _stats[stat.Id()] = stat;
        return this;
    }
}