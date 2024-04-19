using System;
using UnityEngine;

public sealed class Warrior : IDamageable, IEntity, IMember, IUpdate, IDisposable
{
    public event Action<IUpdate> UpdateRemoveRequested;
    
    public GameObject Prefab => _prefab;
    
    private readonly Team _team;
    private readonly CollisionComponent _collisionComponent;
    private readonly IStats<int> _stats;
    private readonly GameObject _prefab;

    public Warrior(
        Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats, 
        GameObject prefab)
    {
        _team = team;
        _collisionComponent = collisionComponent;
        _stats = stats;
        _prefab = prefab;
    }
    

    public void TakeDamage(int damagePoints)
    {
    }

    public void GameUpdate(float deltaTime)
    {
        
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}