using Game.Attack;
using UnityEngine;

namespace Game.Common.Filters
{
    public class DamageableFilter : FilterDecorator
    {
        public DamageableFilter() : base()
        {
            
        }
        
        public DamageableFilter(IFilter child) : base(child)
        {
        }

        protected override bool CheckInternal(GameObject obj)
        {
            return obj.TryGetComponent<IDamageable>(out _);
        }
    }
}