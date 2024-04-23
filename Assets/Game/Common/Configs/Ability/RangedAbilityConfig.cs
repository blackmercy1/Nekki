using Game.Systems.AbilitySystem;
using UnityEngine;

namespace Game.Common.Configs.Ability
{
    [CreateAssetMenu(fileName = "RangedAbilityConfig", menuName = "ScriptableObject/Configs/Abilities/RangedAbility")]
    public class RangedAbilityConfig : ScriptableObject
    {
        [SerializeField] private RangedAbility _rangedAbilityPrefab;
        [SerializeField] private int _flySpeed;
        [SerializeField] private int _damage;

        public RangedAbility RangedAbility => _rangedAbilityPrefab;
        public int FlySpeed => _flySpeed;
        public int Damage => _damage;
    }
}