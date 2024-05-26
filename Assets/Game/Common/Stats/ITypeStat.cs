namespace Game.Common.Stats
{
    public interface ITypeStat<T> : IStat<T>
    {
        string Id();
    }
}