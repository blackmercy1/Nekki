public sealed class Warrior : IEntity, IDamageable
{
    public CollisionComponent CollisionComponent { get; }
    public MemberTeams Teams { get;}
    
    private IStats<int> _stats;

    public Warrior(MemberTeams teams, CollisionComponent collisionComponent, IStats<int> stats)
    {
        Teams = teams;
        CollisionComponent = collisionComponent;
        _stats = stats;
    }
    
    public void Method()
    {
        
    }

    public void TakeDamage(int damagePoints)
    {
    }
}