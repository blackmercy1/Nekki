using UnityEngine;

public sealed class WizardEntityCreator : EnityCreator<Wizard>
{
    private readonly WizardConfig _config;

    public WizardEntityCreator(WizardConfig config, GameObject prefab)
    {
        Prefab = prefab;
        _config = config;
    }
    
    public override Wizard CreateEntity()
    {
        Prefab.TryGetComponent<CollisionComponent>(out var collisionComponent);
        var gameObject = Instantiator.InstantiateGameObject(Prefab);
        
        var stats = new Stats()
            .Add(new Health("health", _config.HealthValue))
            .Add(new Damage("damage", _config.DamageValue))
            .Add(new Defence("defence", _config.DefenceValue))
            .Add(new MovementSpeed("movementSpeed", _config.MovementSpeedValueValue));
        
        return new Wizard(
            _config.Team,
            collisionComponent,
            stats,
            gameObject);
    }
}