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
        var gameObject = Object.Instantiate(_config.Prefab);;
        gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);
        
        var stats = new Stats()
            .Add(new Health("health", _config.HealthValue))
            .Add(new Damage("damage", _config.DamageValue))
            .Add(new Defence("defence", _config.DefenceValue))
            .Add(new MovementSpeed("movementSpeed", _config.MovementSpeedValueValue));

        gameObject.Initialize( 
            _config.Team,
            collisionComponent,
            stats);

        return gameObject;
    }
}

//паттерн цепочка обязанностей