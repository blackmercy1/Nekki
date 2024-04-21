using System;
using UnityEngine;

public sealed class Warrior : MonoBehaviour, IDamageable, IMember, IUpdate, IDisposable
{
    public event Action<IUpdate> UpdateRemoveRequested;

    public Team TeamMember => _teamMemberMember;
    
    private CollisionComponent _collisionComponent;
    private ITypeStat<int> _defenceStat;
    private ITypeStat<int> _healthStat;
    private IStats<int> _stats;
    
    private Team _teamMemberMember;

    public void Initialize(
        Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats)
    {
        _teamMemberMember = team;
        _collisionComponent = collisionComponent;
        _stats = stats;

        _healthStat = _stats.Get(StatsConstantIdentifiers.HealthStat);
        _defenceStat = stats.Get(StatsConstantIdentifiers.DefenceStat);
    }
    

    public void TakeDamage(int damagePoints)
    {
        _healthStat.Add(-damagePoints * _defenceStat.GetValue());

        if (_healthStat.GetValue() <= 0)
            DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        Dispose();
    }

    public void GameUpdate(float deltaTime)
    {
        
    }
    public void Dispose()
    {
    }

}