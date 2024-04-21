using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Wizard : MonoBehaviour, IDamageable, IUpdate, IDisposable, IMember
{
    public event Action<IUpdate> UpdateRemoveRequested;
    public event Action Died;

    public Team TeamMember => _teamMember;
    
    private CollisionComponent _collisionComponent;
    private IMovementHandler _movementHandler;
    private IPlayerInputHandler _inputHandler;
    private ITypeStat<int> _healthStat;

    private IAttack _attack;
    private ITypeStat<int> _defenceStat;
    private IStats<int> _stats;
    
    private Team _teamMember;
    
    public void Initialize(
        Team teamMember,
        CollisionComponent collisionComponent,
        IStats<int> stats,
        IMovementHandler movementHandler,
        IPlayerInputHandler inputHandler,
        MeleeAttack attack)
    {
        _teamMember = teamMember;
        _collisionComponent = collisionComponent;
        _stats = stats;
        _movementHandler = movementHandler;
        
        _inputHandler = inputHandler;
        _attack = attack;
        
        _healthStat = _stats.Get(StatsConstantIdentifiers.HealthStat);
        _defenceStat = stats.Get(StatsConstantIdentifiers.DefenceStat);
        
        collisionComponent.CollisionReaction += Attack;
    }
    

    private void Attack(GameObject obj)
    {
        _attack.Attack(obj);
    }

    public void TakeDamage(int damagePoints)
    {
        _healthStat.Add(-damagePoints * _defenceStat.GetValue());

        if (_healthStat.GetValue() <= 0)
            DestroySelf();
    }

    private void DestroySelf()
    {
        Died?.Invoke();
        Dispose();
        Destroy(gameObject);
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

public interface IStateSwitcher
{
    void SwitchState<T>() where T : AbilityBaseState;
}

public abstract class AbilityBaseState
{
    protected readonly IStateSwitcher _stateSwitcher;

    protected AbilityBaseState(IStateSwitcher stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
    }

    public virtual void Start()
    {
        
    }

    public virtual void Stop()
    {
    }
}

public class CastAbilityState : AbilityBaseState
{
    private readonly AbilityHolder _abilityHolder;
    private readonly GameUpdates _gameUpdates;

    public CastAbilityState(
        IStateSwitcher stateSwitcher,
        AbilityHolder abilityHolder,
        GameUpdates gameUpdates) 
        : base(stateSwitcher)
    {
        _abilityHolder = abilityHolder;
        _gameUpdates = gameUpdates;
    }

    public override void Start()
    {
        var ability = _abilityHolder.GetSpell();
        _gameUpdates.Add(ability);
        _stateSwitcher.SwitchState<IdleState>();
    }

    public override void Stop()
    {
    }
}

public class IdleState : AbilityBaseState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly IPlayerInputHandler _playerInputHandler;

    public IdleState(
        IStateSwitcher stateSwitcher,
        IPlayerInputHandler playerInputHandler)
        : base(stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
        _playerInputHandler = playerInputHandler;
    }

    public override void Start()
    {
        Debug.LogError("подписка");
        _playerInputHandler.AbilityActivated += Cast;
        _playerInputHandler.AbilityNextActivated += SetNextSpellState;
        _playerInputHandler.AbilityPreviousActivated += SetPreviousSpellState;
    }
    
    public override void Stop()
    {
        Debug.LogError("атписка");
        _playerInputHandler.AbilityActivated -= Cast;
        _playerInputHandler.AbilityNextActivated -= SetNextSpellState;
        _playerInputHandler.AbilityPreviousActivated -= SetPreviousSpellState;
    }
    
    private void SetNextSpellState() => _stateSwitcher.SwitchState<SetNextSpellState>();
    private void SetPreviousSpellState() => _stateSwitcher.SwitchState<SetPreviousSpellState>();
    private void Cast() => _stateSwitcher.SwitchState<CastAbilityState>(); 
}

public class SetNextSpellState : AbilityBaseState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly AbilityHolder _holder;

    public SetNextSpellState(
        IStateSwitcher stateSwitcher,
        AbilityHolder holder) : 
        base(stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
        _holder = holder;
    }

    public override void Start()
    {
        _holder.SetNextAbility();
        _stateSwitcher.SwitchState<IdleState>();
    }
}

public class SetPreviousSpellState : AbilityBaseState
{
    private readonly IStateSwitcher _stateSwitcher;
    private readonly AbilityHolder _holder;

    public SetPreviousSpellState(
        IStateSwitcher stateSwitcher,
        AbilityHolder holder) : 
        base(stateSwitcher)
    {
        _stateSwitcher = stateSwitcher;
        _holder = holder;
    }

    public override void Start()
    {
        _holder.SetPreviousAbility();
        _stateSwitcher.SwitchState<IdleState>();
    }
}

public class AbilityContext : IStateSwitcher
{
    private readonly GameUpdates _gameUpdates;
    private readonly List<AbilityBaseState> _allStates;
    
    private AbilityBaseState _currentState;

    public AbilityContext(
        AbilityHolder abilityHolder,
        IPlayerInputHandler playerInputHandler, 
        GameUpdates gameUpdates)
    {
        _gameUpdates = gameUpdates;
        _allStates = new List<AbilityBaseState>
        {
            new IdleState(this, playerInputHandler),
            new CastAbilityState(this, abilityHolder, _gameUpdates),
            new SetNextSpellState(this, abilityHolder),
            new SetPreviousSpellState(this, abilityHolder)
        };
        
        _currentState = _allStates[0];
        _currentState.Start();
    }
    
    public void SwitchState<T>() where T : AbilityBaseState
    {
        _currentState.Stop();
        _currentState = _allStates.FirstOrDefault(t => t is T);
        _currentState.Start();
    }
}

