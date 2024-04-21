using System.Collections.Generic;
using Game;
using Object = UnityEngine.Object;

public sealed class WizardEntityCreator : EnityCreator<Wizard>
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

    public override Wizard CreateEntity()
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

public class AbilityHolder
{
    private readonly List<AbilityCreator> _abilityCreators;

    private AbilityCreator _currentAbilityCreator;
    private int _currentAbilityIndex;

    public AbilityHolder(List<AbilityCreator> abilityCreators)
    {
        _abilityCreators = abilityCreators;
        _currentAbilityCreator = abilityCreators[0];
        _currentAbilityIndex = 0;
    }

    public Ability GetSpell() => _currentAbilityCreator.CreateAbility();

    public void SetNextAbility()
    {
        if (_currentAbilityIndex + 1 > _abilityCreators.Count - 1)
        {
            ChangeIndexAbility(0);
        }

        ChangeIndexAbility(_currentAbilityIndex + 1);
    }

    public void SetPreviousAbility()
    {
        if (_currentAbilityIndex - 1 < 0)
        {
            ChangeIndexAbility(_abilityCreators.Count - 1);
        }

        ChangeIndexAbility(_currentAbilityIndex - 1);
    }

    private void ChangeIndexAbility(int index)
    {
        _currentAbilityIndex = index;
        _currentAbilityCreator = _abilityCreators[_currentAbilityIndex];
    }
}