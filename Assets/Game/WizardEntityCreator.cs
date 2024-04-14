public sealed class WizardEntityCreator : EnityCreator
{
    private readonly WizardConfig _wizardConfig;

    public WizardEntityCreator(WizardConfig wizardConfig)
    {
        _wizardConfig = wizardConfig;
    }
    
    public override IEntity CreateEntity()
    {
        return new Wizard(
            _wizardConfig.Teams,
            _wizardConfig.CollisionComponent,
            _wizardConfig.Stats);
    }
}