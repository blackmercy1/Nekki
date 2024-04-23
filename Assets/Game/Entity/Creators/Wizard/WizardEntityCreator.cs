using Game.Common.Collision;
using Game.Common.Configs.Ability;
using Game.Common.Configs.Entity.Wizard;
using Game.Common.Filters;
using Game.Common.Stats;
using Game.Core.Input;
using Game.Systems.AttackSystem;
using Game.Systems.MovementSystem;
using Object = UnityEngine.Object;

namespace Game.Entity.Creators.Wizard
{
    public sealed class WizardEntityCreator : EnityCreator<Entity.Wizard>
    {
        private readonly WizardConfig _config;
        private readonly IPlayerInputHandler _inputHandler;
        private readonly RangedAbilityConfig _rangedAbilityConfig;
        private readonly FireAbilityConfig _fireAbilityConfig;

        public WizardEntityCreator(
            WizardConfig config,
            IPlayerInputHandler inputHandler,
            RangedAbilityConfig rangedAbilityConfig)
        {
            _config = config;
            _inputHandler = inputHandler;
            _rangedAbilityConfig = rangedAbilityConfig;
        }

        public override Entity.Wizard CreateEntity()
        {
            var gameObject = Object.Instantiate(_config.Prefab);
            gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var teamMember = _config.Team;
            var stats = new Stats()
                .Add(new Health(StatsConstantIdentifiers.HealthStat, _config.HealthValue))
                .Add(new Damage(StatsConstantIdentifiers.DamageStat, _config.DamageValue))
                .Add(new Defence(StatsConstantIdentifiers.DefenceStat, _config.DefenceValue))
                .Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _config.MovementSpeedValue))
                .Add(new RotationSpeed(StatsConstantIdentifiers.RotationSpeed, _config.RotationSpeedValue));

            stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeed);
            stats.TryGet(StatsConstantIdentifiers.DamageStat, out var damageStat);
            stats.TryGet(StatsConstantIdentifiers.RotationSpeed, out var rotationSpeed);

            var movementHandler = new PlayerMovementHandler(
                _inputHandler,
                gameObject.transform,
                movementSpeed,
                rotationSpeed);

            var damageableFilter = new DamageableFilter();
            var memberFilter = new MemberFilter(damageableFilter, teamMember, differenceFlag: true);
            var attack = new MeleeAttack(memberFilter, damageStat);
        
            gameObject.Initialize(
                _config.Team,
                collisionComponent,
                stats,
                movementHandler,
                _inputHandler,
                attack);

            return gameObject;
        }
    }
}