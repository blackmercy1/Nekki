using System.Collections.Generic;
using UnityEngine;

public sealed class WizardGenerator
{
    private readonly WizardConfig _wizardConfig;
    private readonly IPlayerInputHandler _inputHandler;
    private readonly GameUpdates _gameUpdates;
    private readonly Transform _sceneStartPosition;
    private readonly RangedAbilityConfig _rangedAbilityConfig;
    private readonly FireAbilityConfig _fireAbilityConfig;

    public WizardGenerator(
        WizardConfig wizardConfig,
        IPlayerInputHandler inputHandler,
        GameUpdates gameUpdates,
        Transform sceneStartPosition,
        RangedAbilityConfig rangedAbilityConfig, 
        FireAbilityConfig fireAbilityConfig)
    {
        _wizardConfig = wizardConfig;
        _inputHandler = inputHandler;
        _gameUpdates = gameUpdates;
        _sceneStartPosition = sceneStartPosition;
        _rangedAbilityConfig = rangedAbilityConfig;
        _fireAbilityConfig = fireAbilityConfig;

        SpawnWizard();
    }
    
    private void SpawnWizard()
    {
        var entity = new WizardEntityCreator(
            _wizardConfig,
            _inputHandler,
            _rangedAbilityConfig).CreateEntity();
        
        CreateWizardAbilities(entity);
        
        var handler = CreateWizardHandler();
        handler.Handle(entity);
    }

    private AbilityContext CreateWizardAbilities(Wizard entity)
    {
        var rangedAbility = new RangedAbilityCreator(entity.TeamMember, _rangedAbilityConfig, entity.transform);
        var fireAbility = new FireAbilityCreator(entity.TeamMember, _fireAbilityConfig, entity.transform);
        var abilityHolder = new AbilityHolder(new List<AbilityCreator> {fireAbility, rangedAbility});
        var abilityContext = new AbilityContext(abilityHolder, _inputHandler, _gameUpdates);
        
        return abilityContext;
    }

    //todo : сюда можно запихать пул чтобы была проверка на то сколько сейчас врагов на сцене 
    
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
