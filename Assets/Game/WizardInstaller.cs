using UnityEngine;

public sealed class WizardInstaller : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private WizardConfig _wizardConfig;
    //должно быть в отдельном конфиге
    [SerializeField] private Transform _sceneStartPosition;
    
    private WizardEntityCreator _entityCreator;
    private bool _isInitialized = false;

    private void Awake() => Install();
    
    public void Initialize()
    {
        if (_isInitialized)
            return;
        if (!_isInitialized)
            _isInitialized = !_isInitialized;
    }

    //todo : сюда можно запихать пул чтобы была проверка на то сколько сейчас врагов на сцене 
    private void GeneratePlayer()
    {
        var handler = HandlerSpawnWarrior();
        handler.Handle(_entityCreator);
    }

    private void Install()
    {
    }
    
    private IHandler<Wizard> HandlerSpawnWarrior()
    {
        _entityCreator = new WizardEntityCreator(_wizardConfig, _prefab);
        
        var generatorHandler = new EnemyGeneratorHandle<Wizard>();
        var positionHandler = new PositionHandler<Wizard>(_sceneStartPosition.position);
        
        generatorHandler.SetNext(positionHandler);
        
        return generatorHandler;
    }

    private void DestroyInstaller()
    {
        Destroy(gameObject);
    }
}

public class InputFromKeyboard : IInputHandler
{
    public Vector3 PlayerInputDirection => _playerInputDirection;
    private Vector3 _playerInputDirection;
    
    public void GetInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        _playerInputDirection = new Vector3(horizontalInput, 0, verticalInput);
    }
}