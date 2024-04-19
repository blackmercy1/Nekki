public interface IStats<T>
{
    ITypeStat<T> Get(string id);
    bool TryGet(string id, out ITypeStat<T> stat);
    IStats<T> Add(ITypeStat<T> stat);
}