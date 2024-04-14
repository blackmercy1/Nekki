public sealed class WarriorEntityCreator : EnityCreator
{
    private readonly WarriorConfig _config;
    
    public WarriorEntityCreator(WarriorConfig config)
    {
        _config = config;
    }

    public override IEntity CreateEntity()
    {
        return new Warrior(
            _config.Teams,
            _config.CollisionComponent,
            _config.Stats);
    }
}