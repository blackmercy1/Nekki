namespace Game.Common.ResponabilityChain
{
    public interface IHandler<T>
    {
        IHandler<T> SetNext(IHandler<T> handler);
        T Handle(T obj);
    }
}