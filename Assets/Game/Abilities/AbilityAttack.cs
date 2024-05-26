using Game.Attack;
using Game.Common.Filters;
using Game.Common.Stats;
using UnityEngine;

namespace Game.Abilities
{
    public class AbilityAttack : IAttack
    {
        private readonly FilterDecorator _filter;
        private readonly ITypeStat<int> _damageStat;

        public AbilityAttack(
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