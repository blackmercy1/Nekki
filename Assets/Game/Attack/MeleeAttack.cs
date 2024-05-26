using Game.Common.Filters;
using Game.Common.Stats;
using UnityEngine;

namespace Game.Attack
{
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

        public bool Attack(GameObject gameObject)
        {
            if (_filter.Check(gameObject))
            {
                gameObject.TryGetComponent<IDamageable>(out var damageable);
                damageable.TakeDamage(_damageStat.GetValue());
                return true;
            }

            return false;
        }
    }
}