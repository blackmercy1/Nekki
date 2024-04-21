using UnityEngine;

//желательно сделать Generic для enemy
//Все инсталерры должны удаляться, но сейчас на него подвязан ключевой ивент, сделано для упрощения 
public sealed class WarriorInstaller : MonoBehaviour
{
    [SerializeField] private WarriorConfig _warriorConfig;
    
    private WarriorEntityCreator _warriorEntityCreator;
    private GameUpdates _gameUpdates;
    private RandomLoopTimer _timer;
    private GameArea _gameArea;
    
    private bool _isInitialized;
    
    public void Initialize(
        RandomLoopTimer timer,
        GameArea gameArea,
        GameUpdates gameUpdates)
    {
        if (_isInitialized)
            return;
        if (!_isInitialized)
            _isInitialized = !_isInitialized;
        
        _timer = timer;
        _gameArea = gameArea;
        _gameUpdates = gameUpdates;

        var entityGenerator = new EntityGenerator(
            _timer,
            _gameArea,
            _gameUpdates,
            _warriorConfig);
    }
}