using System.Collections.Generic;
using Game.Abilities;
using Game.Abilities.FireAbility;
using Game.Abilities.RangedAbility;
using Game.Attack;
using Game.Common.Collision;
using Game.Common.Filters;
using Game.Common.ResponabilityChain;
using Game.Common.Stats;
using Game.Core.Input;
using Game.Core.Updates;
using Game.Entity;
using Game.Positions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Wizard
{
    public sealed class WizardEntityCreator : EntityCreator<Wizard>
    {
        private readonly WizardConfig _wizardConfig;
        private readonly RangedAbilityConfig _rangedAbilityConfig;
        private readonly GameUpdates _gameUpdates;
        private readonly Transform _sceneStartPosition;
        private readonly FireAbilityConfig _fireAbilityConfig;

        public WizardEntityCreator(
            WizardConfig wizardWizardConfig,
            GameUpdates gameUpdates,
            Transform sceneStartPosition,
            RangedAbilityConfig rangedAbilityConfig, 
            FireAbilityConfig fireAbilityConfig)
        {
            _wizardConfig = wizardWizardConfig;
            _gameUpdates = gameUpdates;
            _sceneStartPosition = sceneStartPosition;
            _rangedAbilityConfig = rangedAbilityConfig;
            _fireAbilityConfig = fireAbilityConfig;
        }

        public override Wizard CreateEntity()
        {
            var wizard = Object.Instantiate(_wizardConfig.Prefab);
            
            wizard.TryGetComponent<CollisionComponent>(out var collisionComponent);

            var teamMember = _wizardConfig.Team;
            var stats = CreateStats();

            stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementSpeed);
            stats.TryGet(StatsConstantIdentifiers.DamageStat, out var damageStat);
            stats.TryGet(StatsConstantIdentifiers.RotationSpeed, out var rotationSpeed);

            var directionInput = new AxisDirectionInput(
                new AxisInput(_wizardConfig.XAxisName),
                new EmptyAxisInput(),
                new AxisInput(_wizardConfig.YAxisName));
            
            var movementHandler = new PlayerMovement(
                directionInput,
                wizard.transform,
                movementSpeed,
                rotationSpeed);

            var damageableFilter = new DamageableFilter();
            var memberFilter = new MemberFilter(damageableFilter, teamMember, differenceFlag: true);
            var attack = new MeleeAttack(memberFilter, damageStat);
            
            var abilityContext = CreateWizardAbilities(teamMember, wizard.transform);
            
            wizard.Initialize(
                teamMember,
                collisionComponent,
                stats,
                movementHandler,
                attack,
                abilityContext);
            
            
            var handler = CreateWizardHandler();
            handler.Handle(wizard);

            return wizard;
        }

        private IStats<int> CreateStats()
        {
            var stats = new Stats()
                .Add(new Health(StatsConstantIdentifiers.HealthStat, _wizardConfig.HealthValue))
                .Add(new Damage(StatsConstantIdentifiers.DamageStat, _wizardConfig.DamageValue))
                .Add(new Defence(StatsConstantIdentifiers.DefenceStat, _wizardConfig.DefenceValue))
                .Add(new MovementSpeed(StatsConstantIdentifiers.MovementSpeed, _wizardConfig.MovementSpeedValue))
                .Add(new RotationSpeed(StatsConstantIdentifiers.RotationSpeed, _wizardConfig.RotationSpeedValue));
            
            return stats;
        }

        private AbilityContext CreateWizardAbilities(Team teamMember, Transform originTransform)
        {
            var activateAbilityButton = new KeyboardButtonInput(_wizardConfig.ActivateAbilityButton);
            var activateNextAbilityButton = new KeyboardButtonInput(_wizardConfig.ActivateNextAbilityButton);
            var activatePreviousAbilityButton = new KeyboardButtonInput(_wizardConfig.ActivatePreviousAbilityButton);
            
            var rangedAbility = new RangedAbilityCreator(
                teamMember,
                _rangedAbilityConfig,
                originTransform,
                _gameUpdates);
            
            var fireAbility = new FireAbilityCreator(
                teamMember,
                _fireAbilityConfig,
                originTransform,
                _gameUpdates);
            
            var abilityHolder = new AbilityHolder(new List<AbilityCreator> {fireAbility, rangedAbility});
            var abilityContext = new AbilityContext(
                abilityHolder,
                _gameUpdates,
                activateAbilityButton,
                activateNextAbilityButton,
                activatePreviousAbilityButton);
        
            return abilityContext;
        }
        
        private IHandler<Wizard> CreateWizardHandler()
        {
            var staticPositionGenerator = new GenerateConstantPosition(_sceneStartPosition.position);
        
            var startNode = new StartNode<Wizard>();
            var updateNode = new AddToUpdateNode<Wizard>(_gameUpdates);
            var positionNode = new PlaceEntityNode<Wizard>(staticPositionGenerator);
        
            startNode
                .SetNext(updateNode)
                .SetNext(positionNode);
        
            return startNode;
        }
    }
}