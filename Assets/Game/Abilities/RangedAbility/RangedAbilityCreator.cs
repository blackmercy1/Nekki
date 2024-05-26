using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Core.Updates;
using Game.Movement;
using UnityEngine;

namespace Game.Abilities.RangedAbility
{
    public class RangedAbilityCreator : AbilityCreator
    {
        private readonly RangedAbilityConfig _config;
        private readonly Team _memberTeamHolder;
        private readonly GameUpdates _gameUpdates;
        private Transform _holder;

        public RangedAbilityCreator(
            Team memberTeamHolder,
            RangedAbilityConfig config,
            Transform holder,
            GameUpdates gameUpdates)
        {
            _memberTeamHolder = memberTeamHolder;
            _config = config;
            _holder = holder;
            _gameUpdates = gameUpdates;
        }

        public override Ability CreateAbility()
        {
            var rangedAbility = Object.Instantiate(_config.RangedAbility);
            rangedAbility.transform.position = _holder.position + _holder.forward * 0.5f;
            rangedAbility.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var stats = CreateStats();

            stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeedValue);
            stats.TryGet(StatsConstantIdentifiers.DamageStat, out var damageValue);
        
            var damageableFilter = new DamageableFilter();
            var memberFilter = new MemberFilter(
                damageableFilter,
                _memberTeamHolder,
                differenceFlag:true);
        
            var abilityAttack = new AbilityAttack(memberFilter, damageValue);

            var movementHandler = new ForwardMovement(
                rangedAbility.transform,
                movementSpeedValue,
                _holder.rotation);
        
            rangedAbility.Initialize(
                999,
                movementHandler,
                stats,
                collisionComponent,
                abilityAttack);
            
            _gameUpdates.Add(rangedAbility);
        
            return rangedAbility;
        }

        private Stats CreateStats()
        {
            var stats = new Stats();

            stats
                .Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.FlySpeed))
                .Add(new MovementSpeed(StatsConstantIdentifiers.DamageStat, _config.Damage));
            return stats;
        }
    }
}