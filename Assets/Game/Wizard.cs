using System;
using UnityEngine;

public sealed class Wizard : IEntity, IDamageable, IUpdate
{
    public event Action<IUpdate> UpdateRemoveRequested;
    public CollisionComponent CollisionComponent { get; }
    public Team Team { get; }

    private IStats<int> _stats;
    
    public Wizard(Team team, CollisionComponent collisionComponent, IStats<int> stats)
    {
        CollisionComponent = collisionComponent;
        Team = team;
        _stats = stats;
    }
    
    public void Method()
    {
        
    }

    public void TakeDamage(int damagePoints)
    {
        
    }

    public void GameUpdate(float deltaTime)
    {
        throw new NotImplementedException();
    }

}