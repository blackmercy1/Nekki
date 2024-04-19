public sealed class WizardEntityCreator : EnityCreator<Wizard>
{
    private readonly WizardConfig _config;
    private readonly IInputHandler _inputHandler;
    private IMovementHandler _movementHandler;

    public WizardEntityCreator(
        WizardConfig config,
        IInputHandler inputHandler)
    {
        _config = config;
        _inputHandler = inputHandler;   
    }
    
    public override Wizard CreateEntity()
    {
        var gameObject = Instantiator.InstantiateGameObject(_config.Prefab);
        gameObject.AddComponent<IMember>();
        gameObject.TryGetComponent<CollisionComponent>(out var collisionComponent);
        
        var stats = new Stats()
            .Add(new Health("health", _config.HealthValue))
            .Add(new Damage("damage", _config.DamageValue))
            .Add(new Defence("defence", _config.DefenceValue))
            .Add(new MovementSpeed("movementSpeed", _config.MovementSpeedValue));

        stats.TryGet(StatsConstantIdentifiers.MovementSpeed, out var movementsSpeed);
        
        _movementHandler = new PlayerMovementHandler(
            _inputHandler, 
            gameObject.transform,
            movementsSpeed.GetValue());
        
        return new Wizard(
            _config.Team,
            collisionComponent,
            stats,
            _movementHandler,
            gameObject);
    }
}

public static class StatsConstantIdentifiers
{
    public static readonly string HealthStat = "health";
    public static readonly string DamageStat = "damage";
    public static readonly string DefenceStat = "defence";
    public static readonly string MovementSpeed = "movementSpeed";
}