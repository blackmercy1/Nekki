using Game.Common.Collision;
using Game.Common.Configs.Ability;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Systems.AttackSystem;
using Game.Systems.MovementSystem;
using UnityEngine;

namespace Game.Systems.AbilitySystem.Creator
{
    public class RangedAbilityCreator : AbilityCreator
    {
        private readonly RangedAbilityConfig _config;
        private readonly Team _memberTeamHolder;
        private Transform _holder;
    
        public RangedAbilityCreator(
            Team memberTeamHolder,
            RangedAbilityConfig config,
            Transform holder)
        {
            _memberTeamHolder = memberTeamHolder;
            _config = config;
            _holder = holder;
        }

        public override Ability CreateAbility()
        {
            var gameObject = Object.Instantiate(_config.RangedAbility);
            gameObject.transform.position = _holder.position;
            gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var stats = new Stats();
            stats.Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.FlySpeed))
                .Add(new MovementSpeed(StatsConstantIdentifiers.DamageStat, _config.Damage));
            stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeedValue);
            stats.TryGet(StatsConstantIdentifiers.DamageStat, out var damageValue);
        
            var damageableFilter = new DamageableFilter();
            var memberFilter = new MemberFilter(
                damageableFilter,
                _memberTeamHolder,
                differenceFlag:true);
        
            var abilityAttack = new AbilityAttack(memberFilter, damageValue);

            var movementHandler = new ForwardMovement(
                gameObject.transform,
                movementSpeedValue,
                _holder.rotation);
        
            gameObject.Initialize(
                999,
                movementHandler,
                stats,
                collisionComponent,
                abilityAttack);
        
            return gameObject;
        }
    }
}