using System;
using UnityEngine;

public sealed class Wizard : IDamageable, IUpdate
{
    public event Action<IUpdate> UpdateRemoveRequested;
    
    private readonly CollisionComponent _collisionComponent;
    private readonly IStats<int> _stats;
    private readonly GameObject _gameObject;
    private readonly Team _teams;

    public Wizard(
        Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats,
        GameObject gameObject)
    {
        _collisionComponent = collisionComponent;
        _teams = team;
        _stats = stats;
        _gameObject = gameObject;
    }
    
    public void TakeDamage(int damagePoints)
    {
        
    }

    public void GameUpdate(float deltaTime)
    {
    }
}