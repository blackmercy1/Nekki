using UnityEngine;

public abstract class AbstractNodeHandler<T> : IHandler<T> where T : MonoBehaviour
{
    private IHandler<T> _handler;

    public IHandler<T> SetNext(IHandler<T> handler)
    {
        _handler = handler;
        return _handler;
    }

    public virtual T Handle(T obj) => _handler?.Handle(obj);
}