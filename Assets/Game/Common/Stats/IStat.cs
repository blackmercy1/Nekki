namespace Game.Common.Stats
{
    public interface IStat<T>
    {
        T GetValue();

        void Add(T value);
    }
}