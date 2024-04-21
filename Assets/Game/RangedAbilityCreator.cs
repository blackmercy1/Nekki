using Game;
using UnityEngine;

public class FireAbilityCreator : AbilityCreator
{
    private readonly FireAbilityConfig _config;
    private readonly Transform _holder;
    private readonly Team _memberTeamHolder;
    
    public FireAbilityCreator(
        Team memberTeamHolder,
        FireAbilityConfig config, 
        Transform holder)
    {
        _memberTeamHolder = memberTeamHolder;
        _config = config;
        _holder = holder;
    }

    public override Ability CreateAbility()
    {
        var gameObject = Object.Instantiate(_config.RangedAbility);
        gameObject.transform.position = _holder.position + Vector3.forward;
        gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);

        var stats = new Stats();
        stats.Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.FlySpeed));
        stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeed);
        
        var damageableFilter = new DamageableFilter();
        var memberFilter = new MemberFilter(damageableFilter, _memberTeamHolder, differenceFlag:true);

        var movementHandler = new ForwardMovement(gameObject.transform, movementSpeed);
        gameObject.Initialize(
            999,
            movementHandler,
            stats,
            collisionComponent,
            memberFilter);
        
        return gameObject;
    }
}

public abstract class AbilityCreator
{
    public abstract Ability CreateAbility();
}


public class RangedAbilityCreator : AbilityCreator
{
    private readonly RangedAbilityConfig _config;
    private readonly Team _memberTeamHolder;
    private Transform _holder;
    
    public RangedAbilityCreator(
        Team memberTeamHolder,
        RangedAbilityConfig config,
        Transform holder)
    {
        _memberTeamHolder = memberTeamHolder;
        _config = config;
        _holder = holder;
    }

    public override Ability CreateAbility()
    {
        var gameObject = Object.Instantiate(_config.RangedAbility);
        gameObject.transform.position = _holder.position;
        gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);

        var stats = new Stats();
        stats.Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.FlySpeed));
        stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeed);
        
        var damageableFilter = new DamageableFilter();
        var memberFilter = new MemberFilter(damageableFilter, _memberTeamHolder, differenceFlag:true);

        var movementHandler = new ForwardMovement(gameObject.transform, movementSpeed);
        gameObject.Initialize(
            999,
            movementHandler,
            stats,
            collisionComponent,
            memberFilter);
        
        return gameObject;
    }
}