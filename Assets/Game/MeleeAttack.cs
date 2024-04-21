using UnityEngine;

public class MeleeAttack : IAttack
{
    private readonly FilterDecorator _filter;
    private readonly ITypeStat<int> _damageStat;

    public MeleeAttack(
        FilterDecorator filter,
        ITypeStat<int> damageStat)
    {
        _filter = filter;
        _damageStat = damageStat;
    }

    public void Attack(GameObject gameObject)
    {
        if (_filter.Check(gameObject))
        {
            gameObject.TryGetComponent<IDamageable>(out var damageable);
            //фильтр уже проверил на idamageable, поэтому ? не требуется
            damageable.TakeDamage(_damageStat.GetValue());
        }
    }
}