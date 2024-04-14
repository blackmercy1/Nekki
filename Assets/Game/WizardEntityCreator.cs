public sealed class WizardEntityCreator : EnityCreator<Wizard>
{
    private readonly WizardConfig _wizardConfig;

    public WizardEntityCreator(WizardConfig wizardConfig)
    {
        _wizardConfig = wizardConfig;
    }
    
    public override Wizard CreateEntity()
    {
        return new Wizard(
            _wizardConfig.Team,
            _wizardConfig.CollisionComponent,
            _wizardConfig.Stats);
    }
}