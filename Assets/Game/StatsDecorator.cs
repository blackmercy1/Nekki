public abstract class StatsDecorator<T> : IStat<T>
{
    protected readonly IStat<T> Child;
    
    public StatsDecorator(IStat<T> child)
    {
        Child = child;
    }

    public abstract T GetValue();

    public abstract void Add(T value);
}