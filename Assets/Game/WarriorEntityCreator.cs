using System;
using UnityEngine;

public sealed class WarriorEntityCreator : EnityCreator<Warrior>
{
    private readonly WarriorConfig _config;
    
    public WarriorEntityCreator(WarriorConfig config, GameObject prefab)
    {
        _config = config;
        Prefab = prefab;
    }
    
    public override Warrior CreateEntity()
    {
        Prefab.TryGetComponent<CollisionComponent>(out var collisionComponent);
        var gameObject = Instantiator.InstantiateGameObject(Prefab);
        
        var stats = new Stats()
            .Add(new Health("health", _config.HealthValue))
            .Add(new Damage("damage", _config.DamageValue))
            .Add(new Defence("defence", _config.DefenceValue))
            .Add(new MovementSpeed("movementSpeed", _config.MovementSpeedValueValue));
        
        return new Warrior(
            _config.Team,
            collisionComponent,
            stats,
            gameObject);
    }
}

//паттерн цепочка обязанностей
public interface IHandler<T>
{
    IHandler<T> SetNext(IHandler<T> handler);
    T Handle(object obj);
}

public abstract class AbstractHandler<T> : IHandler<T>
{
    private IHandler<T> _handler;

    public IHandler<T> SetNext(IHandler<T> handler)
    {
        _handler = handler;
        return _handler;
    }

    public virtual T Handle(object obj) => _handler != null ? _handler.Handle(obj) : default;
}

public sealed class EnemyGeneratorHandle<T> : AbstractHandler<T>
{
    public event Action<T> Spawned;
    
    
    public override T Handle(object obj)
    {
        if (obj is EnityCreator<T> entityCreator)
        {
            var entity = entityCreator.CreateEntity();
            Spawned?.Invoke(entity);
            return base.Handle(entityCreator.Prefab);
        }

        return default;
    }
}

public interface IPlacer<in T> where T : MonoBehaviour
{
    void Place(T gameObject);
}

public sealed class PositionHandler<T> : AbstractHandler<T> 
{
    private readonly GameArea _gameArea;

    public PositionHandler(GameArea gameArea)
    {
        _gameArea = gameArea;
    }

    public override T Handle(object obj)
    {
        if (obj is GameObject gameObject)
        {
            var startPosition = _gameArea.GetRandomStartPosition();
            gameObject.transform.position = startPosition;
            return base.Handle(obj);
        }
        Debug.LogError($"request is null + value {obj}");
        return default;
    }
}

