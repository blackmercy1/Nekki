using System;
using Game;
using UnityEngine;

public sealed class Wizard : IDamageable, IUpdate, IEntity, IDisposable, IMember
{
    public event Action Died;
    public event Action<IUpdate> UpdateRemoveRequested;
    public GameObject Prefab { get; }
    
    private readonly CollisionComponent _collisionComponent;
    private readonly IMovementHandler _movementHandler;
    private readonly IInputHandler _inputHandler;
    private readonly ITypeStat<int> _healthStat;
    
    private readonly ITypeStat<int> _defenceStat;
    private readonly IStats<int> _stats;
    private readonly Team _teams;

    private IAttack _attackComponent;

    public Wizard(
        Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats,
        IMovementHandler movementHandler,
        GameObject gameObject)
    {
        _collisionComponent = collisionComponent;
        _teams = team;
        _stats = stats;
        _movementHandler = movementHandler;
        
        Prefab = gameObject;
        
        _healthStat = _stats.Get(StatsConstantIdentifiers.HealthStat);
        _defenceStat = stats.Get(StatsConstantIdentifiers.DefenceStat);
        
        InitializeAttackFilter();
        collisionComponent.CollisionReaction += Attack;
    }

    ~Wizard()
    {
        Dispose();
    }

    private void InitializeAttackFilter()
    {
        var damageableFilter = new DamageableFilter();
        var memberFilter = new MemberFilter(damageableFilter);
        _attackComponent = new MeleeAttack(memberFilter);
    }

    private void Attack(GameObject gameObject)
    {
        _attackComponent.Attack(gameObject);
    }

    public void TakeDamage(int damagePoints)
    {
        _healthStat.Add(-damagePoints * _defenceStat.GetValue());
        
        if (_healthStat.GetValue() <= 0)
            Died?.Invoke();
    }

    public void GameUpdate(float deltaTime)
    {
        _movementHandler.Move(deltaTime);
    }

    public void Dispose()
    {
        _collisionComponent.CollisionReaction -= Attack;
    }
}

public interface IAttack
{
    void Attack(GameObject gameObject);
}

public class MeleeAttack : IAttack
{
    private readonly FilterDecorator _filter;
    private readonly int _attackStat;

    public MeleeAttack(FilterDecorator filter)
    {
        _filter = filter;
    }

    public void Attack(GameObject gameObject)
    {
        if (_filter.Check(gameObject))
        {
            gameObject.TryGetComponent<IDamageable>(out var damageable);
            //фильтр уже проверил на idamageable, поэтому ? не требуется
            damageable.TakeDamage(10);
        }
    }
}

public class PlayerMovementHandler : IMovementHandler
{
    private readonly IInputHandler _inputHandler;
    private readonly Transform _transform;
    private readonly int _movementSpeed;

    public PlayerMovementHandler(
        IInputHandler inputHandler,
        Transform transform,
        int movementSpeed)
    {
        _inputHandler = inputHandler;
        _transform = transform;
        _movementSpeed = movementSpeed;
    }
    
    public void Move(float fixedDeltaTime)
    {
        var input = _inputHandler.PlayerInputDirection;
        _transform.position += input * (_movementSpeed * fixedDeltaTime);
    }
}

public interface IMovementHandler
{
    public void Move(float fixedDeltaTime);
}

public interface IInputHandler : IUpdate
{
    Vector3 PlayerInputDirection { get; }
    void GetInput(){}
}