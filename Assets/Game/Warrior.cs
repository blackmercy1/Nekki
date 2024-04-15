using UnityEngine;

public sealed class Warrior : IDamageable
{
    public GameObject GameObject => _gameObject;
    
    private readonly Team _team;
    private readonly CollisionComponent _collisionComponent;
    private readonly IStats<int> _stats;
    private readonly GameObject _gameObject;

    public Warrior(Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats, 
        GameObject gameObject)
    {
        _team = team;
        _collisionComponent = collisionComponent;
        _stats = stats;
        _gameObject = gameObject;
    }
    

    public void TakeDamage(int damagePoints)
    {
    }
}