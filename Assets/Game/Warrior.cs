public sealed class Warrior : IDamageable
{
    private readonly Team _team;
    private readonly CollisionComponent _collisionComponent;
    private readonly IStats<int> _stats;

    public Warrior(
        Team team,
        CollisionComponent collisionComponent,
        IStats<int> stats)
    {
        _team = team;
        _collisionComponent = collisionComponent;
        _stats = stats;
    }
    
    public void Method()
    {
        
    }

    public void TakeDamage(int damagePoints)
    {
    }
}