public interface IStat<T>
{
    T GetValue();

    void Add(T value);
}