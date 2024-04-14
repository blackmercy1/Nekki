public interface IStats<T>
{
    bool TryGet(string id, out ITypeStat<T> stat);
    IStats<T> Add(ITypeStat<T> stat);
}