using System;
using UnityEngine;

public class RangedAbility : Ability, IUpdate
{
    public event Action<IUpdate> UpdateRemoveRequested;
    
    private CollisionComponent _collisionComponent;
    private IMovementHandler _movementHandler;
    private IFilter _filter;
    private Stats _stats;
    
    public void Initialize(
        float duration,
        IMovementHandler movementHandler, 
        Stats stats,
        CollisionComponent collisionComponent,
        IFilter filter)
    {
        Duration = duration;
        _movementHandler = movementHandler;
        _stats = stats;
        _collisionComponent = collisionComponent;
        _filter = filter;

        _collisionComponent.CollisionReaction += Collision;
    }
    
    public override void GameUpdate(float deltaTime)
    {
        _movementHandler.Move(deltaTime);
    }

    private void Collision(GameObject obj)
    {
        if (_filter.Check(obj))
        {
            
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}