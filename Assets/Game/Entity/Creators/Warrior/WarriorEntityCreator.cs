using Game.Common.Collision;
using Game.Common.Configs.Entity.Warrior;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Systems.AttackSystem;
using Game.Systems.MovementSystem;
using UnityEngine;

namespace Game.Entity.Creators.Warrior
{
    public sealed class WarriorEntityCreator : EnityCreator<Entity.Warrior>
    {
        private readonly WarriorConfig _config;
        private readonly Transform _targetTransform;
    
        public WarriorEntityCreator(
            WarriorConfig config,
            Transform targetTransform)
        {
            _config = config;
            _targetTransform = targetTransform;
        }
    
        public override Entity.Warrior CreateEntity()
        {
            var gameObject = Object.Instantiate(_config.Prefab);;
            gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var teamMember = _config.Team;
            var stats = new Stats()
                .Add(new Health(StatsConstantIdentifiers.HealthStat, _config.HealthValue))
                .Add(new Damage(StatsConstantIdentifiers.DamageStat, _config.DamageValue))
                .Add(new Defence(StatsConstantIdentifiers.DefenceStat, _config.DefenceValue))
                .Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.MovementSpeedValueValue));

            stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeedValue);
            stats.TryGet(StatsConstantIdentifiers.DamageStat, out var damageValue);

            var movementHandler = new MoveToTarget(
                gameObject.transform,
                _targetTransform.transform,
            
                movementSpeedValue);
            var damageableFilter = new DamageableFilter();
            var memberFilter = new MemberFilter(damageableFilter, teamMember, differenceFlag: true);

            var attackComponent = new MeleeAttack(memberFilter, damageValue);

            gameObject.Initialize( 
                _config.Team,
                collisionComponent,
                stats,
                movementHandler,
                attackComponent);

            return gameObject;
        }
    }
}