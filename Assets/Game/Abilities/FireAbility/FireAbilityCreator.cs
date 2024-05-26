using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Core.Updates;
using Game.Movement;
using UnityEngine;

namespace Game.Abilities.FireAbility
{
    public class FireAbilityCreator : AbilityCreator
    {
        private readonly FireAbilityConfig _config;
        private readonly Transform _holder;
        private readonly GameUpdates _gameUpdates;
        private readonly Team _memberTeamHolder;
    
        public FireAbilityCreator(
            Team memberTeamHolder,
            FireAbilityConfig config, 
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
            var fireAbility = Object.Instantiate(_config.RangedAbility);
            fireAbility.transform.position = _holder.position + _holder.forward * 0.5f;
            fireAbility.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var stats = new Stats();
            stats
                .Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.FlySpeed))
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
                fireAbility.transform,
                movementSpeedValue,
                _holder.rotation);
        
            fireAbility.Initialize(
                999,
                movementHandler,
                stats,
                collisionComponent,
                abilityAttack);

            _gameUpdates.Add(fireAbility);
        
            return fireAbility;
        }
    }
}