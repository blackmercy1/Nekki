using System;
using UnityEngine;

public sealed class WizardInstaller : MonoBehaviour
{
    //должно быть в отдельном конфиге
    [SerializeField] private WizardConfig _wizardConfig;
    [SerializeField] private Transform _sceneStartPosition;

    private GameUpdates _gameUpdates;
    private IInputHandler _inputHandler;
    
    private bool _isInitialized = false;

    public void Initialize(IInputHandler inputHandler, GameUpdates gameUpdates)
    {
        if (_isInitialized)
            return;
        if (!_isInitialized)
            _isInitialized = !_isInitialized;

        _inputHandler = inputHandler;
        _gameUpdates = gameUpdates;

        var wizardGenerator = new WizardGenerator(
            _wizardConfig,
            _inputHandler,
            _gameUpdates,
            _sceneStartPosition
            );
    }

    private void DestroyInstaller()
    {
        Destroy(gameObject);
    }
}

public sealed class WizardGenerator
{
    private readonly WizardConfig _wizardConfig;
    private readonly IInputHandler _inputHandler;
    private readonly GameUpdates _gameUpdates;
    private readonly Transform _sceneStartPosition;

    public WizardGenerator(
        WizardConfig wizardConfig,
        IInputHandler inputHandler,
        GameUpdates gameUpdates,
        Transform sceneStartPosition)
    {
        _wizardConfig = wizardConfig;
        _inputHandler = inputHandler;
        _gameUpdates = gameUpdates;
        _sceneStartPosition = sceneStartPosition;
        
        SpawnWizard();
    }
    
    private void SpawnWizard()
    {
        var entity = new WizardEntityCreator(_wizardConfig, _inputHandler).CreateEntity();
        var handler = CreateWizardHandler();
        handler.Handle(entity);
    }
    
    //todo : сюда можно запихать пул чтобы была проверка на то сколько сейчас врагов на сцене 
    
    private IHandler<Wizard> CreateWizardHandler()
    {
        var staticPositionGenerator = new GenerateConstantPosition(_sceneStartPosition.position);
        
        var startHandler = new StartNode<Wizard>();
        var updatable = new AddToUpdateNode<Wizard>(_gameUpdates);
        var positionHandler = new PlaceEntityNode<Wizard>(staticPositionGenerator);
        
        startHandler
            .SetNext(updatable)
            .SetNext(positionHandler);
        
        return startHandler;
    }
}

public class AddToUpdateNode<T> : AbstractHandler<T>
{
    private readonly GameUpdates _gameUpdates;

    public AddToUpdateNode(GameUpdates gameUpdates)
    {
        _gameUpdates = gameUpdates;
    }
    
    public override T Handle(T obj)
    {
        if (obj is IUpdate updatable) 
            _gameUpdates.Add(updatable);
        return base.Handle(obj);
    }
}

public class InputFromKeyboard : IInputHandler
{
    public event Action<IUpdate> UpdateRemoveRequested;
    public Vector3 PlayerInputDirection => _playerInputDirection;
    private Vector3 _playerInputDirection;
    
    private void GetInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        _playerInputDirection = new Vector3(horizontalInput, 0, verticalInput);
    }

    public void GameUpdate(float deltaTime)
    {
        GetInput();
    }

}