using Game.Attack;
using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.ResponabilityChain;
using Game.Common.Stats;
using Game.Core.Updates;
using Game.Entity;
using Game.Movement;
using Game.Positions;
using UnityEngine;

namespace Game.Warrior
{
    public sealed class WarriorEntityCreator : EntityCreator<Warrior>
    {
        private readonly WarriorConfig _config;
        private readonly Transform _targetTransform;
        private readonly GameUpdates _gameUpdates;
        private readonly GameArea _gameArea;

        public WarriorEntityCreator(
            WarriorConfig config,
            Transform targetTransform,
            GameUpdates gameUpdates,
            GameArea gameArea)
        {
            _config = config;
            _targetTransform = targetTransform;
            _gameUpdates = gameUpdates;
            _gameArea = gameArea;
        }
    
        public override Warrior CreateEntity()
        {
            var warrior = Object.Instantiate(_config.Prefab);
            warrior.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var teamMember = _config.Team;
            var stats = new Stats()
                .Add(new Health(StatsConstantIdentifiers.HealthStat, _config.HealthValue))
                .Add(new Damage(StatsConstantIdentifiers.DamageStat, _config.DamageValue))
                .Add(new Defence(StatsConstantIdentifiers.DefenceStat, _config.DefenceValue))
                .Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.MovementSpeedValueValue));

            stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeedValue);
            stats.TryGet(StatsConstantIdentifiers.DamageStat, out var damageValue);

            var movementHandler = new MoveToTarget(
                warrior.transform,
                _targetTransform.transform,
                movementSpeedValue);
            
            var handler = SpawnWarriorHandler();
            handler.Handle(warrior);
            
            var damageableFilter = new DamageableFilter();
            var memberFilter = new MemberFilter(damageableFilter, teamMember, differenceFlag: true);
            var attackComponent = new MeleeAttack(memberFilter, damageValue);

            warrior.Initialize( 
                teamMember,
                collisionComponent,
                stats,
                movementHandler,
                attackComponent);

            return warrior;
        }
        
        private IHandler<Warrior> SpawnWarriorHandler()
        {
            var startHandler = new StartNode<Warrior>();
            var updateHandler = new AddToUpdateNode<Warrior>(_gameUpdates);
            var positionHandler = new PlaceEntityNode<Warrior>(_gameArea);

            startHandler
                .SetNext(updateHandler)
                .SetNext(positionHandler);
        
            return startHandler;
        }
    }
}