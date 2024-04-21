using UnityEngine;

public sealed class WizardInstaller : MonoBehaviour
{
    //должно быть в отдельном конфиге
    [SerializeField] private WizardConfig _wizardConfig;
    [SerializeField] private Transform _sceneStartPosition;
    [SerializeField] private RangedAbilityConfig _rangedAbilityConfig;
    [SerializeField] private FireAbilityConfig _fireAbilityConfig;

    private GameUpdates _gameUpdates;
    private IPlayerInputHandler _inputHandler;
    
    private bool _isInitialized = false;

    public void Initialize(
        IPlayerInputHandler inputHandler,
        GameUpdates gameUpdates)
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
            _sceneStartPosition,
            _rangedAbilityConfig,
            _fireAbilityConfig
            );
    }

    private void DestroyInstaller()
    {
        Destroy(gameObject);
    }
}