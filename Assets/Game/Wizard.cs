using System;
using UnityEngine;

public sealed class Placer<T> : IPlacer<T> where T : MonoBehaviour
{
    private readonly GameArea _gameArea;

    public Placer(GameArea gameArea)
    {
        _gameArea = gameArea;
    }
    
    public void Place(T gameObject)
    {
        var transform = gameObject.transform;

        var startPosition = _gameArea.GetRandomStartPosition();

        _gameArea.PlaceObject(transform, startPosition);
    }
}

public interface IPlacer<in T> where T : MonoBehaviour
{
    void Place(T gameObject);
}

public sealed class Wizard : IEntity, IDamageable, IUpdate
{
    public event Action<IUpdate> UpdateRemoveRequested;
    public CollisionComponent CollisionComponent { get; }
    public MemberTeams Teams { get; }

    private IStats<int> _stats;
    
    public Wizard(MemberTeams teams, CollisionComponent collisionComponent, IStats<int> stats)
    {
        CollisionComponent = collisionComponent;
        Teams = teams;
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