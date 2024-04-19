using UnityEngine;

public sealed class WarriorEntityCreator : EnityCreator<Warrior>
{
    private readonly WarriorConfig _config;
    
    public WarriorEntityCreator(WarriorConfig config)
    {
        _config = config;
    }
    
    public override Warrior CreateEntity()
    {
        _config.Prefab.TryGetComponent<CollisionComponent>(out var collisionComponent);
        var gameObject = Instantiator.InstantiateGameObject(_config.Prefab);
        
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
    T Handle(T obj);
}

public abstract class AbstractHandler<T> : IHandler<T>
{
    private IHandler<T> _handler;

    public IHandler<T> SetNext(IHandler<T> handler)
    {
        _handler = handler;
        return _handler;
    }

    public virtual T Handle(T obj) => _handler != null ? _handler.Handle(obj) : default;
}

public sealed class PlaceEntityNode<T> : AbstractHandler<T>
{
    private readonly IPositionGenerator _positionGenerator;
    
    public PlaceEntityNode(IPositionGenerator positionGenerator)
    {
        _positionGenerator = positionGenerator;
    }
    
    public override T Handle(T obj)
    {
        if (obj is not IEntity entity) 
            return default;
        entity.Prefab.transform.position = _positionGenerator.GeneratePosition();
        return base.Handle(obj);
    }
}

public interface IPositionGenerator
{
    Vector3 GeneratePosition();
}

public sealed class StartNode<T> : AbstractHandler<T>
{
}

