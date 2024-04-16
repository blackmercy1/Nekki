using System;
using Ranges;
using UnityEngine;

//желательно сделать Generic для enemy
//Все инсталерры должны удаляться, но сейчас на него подвязан ключевой ивент, сделано для упрощения 
public sealed class WarriorInstaller : MonoBehaviour
{
    [SerializeField] private WarriorConfig _warriorConfig;
    [SerializeField] private GameObject _prefab;

    private WarriorEntityCreator _entityCreator;
    private GameArea _gameArea;
    private RandomLoopTimer _timer;
    private bool _isInitialized = false;

    private void Awake() => Install();
    
    public void Initialize(RandomLoopTimer timer, GameArea gameArea)
    {
        if (_isInitialized)
            return;
        if (!_isInitialized)
            _isInitialized = !_isInitialized;
        
        _timer = timer;
        _gameArea = gameArea;
        
        // это логика находится не в инсталере, сделано для упрощения 
        _timer.TimIsUp += TryToSpawnWarrior;
        _timer.Resume();
    }

    //todo : сюда можно запихать пул чтобы была проверка на то сколько сейчас врагов на сцене 
    private void TryToSpawnWarrior()
    {
        var handler = HandlerSpawnWarrior();
        handler.Handle(_entityCreator);
    }

    private void Install()
    {
    }
    
    private IHandler<Warrior> HandlerSpawnWarrior()
    {
        _entityCreator = new WarriorEntityCreator(_warriorConfig, _prefab);
        
        var generatorHandler = new EnemyGeneratorHandle<Warrior>();
        var gameAreaValueGeneratorHandler = new GameAreaValueGeneratorHandler<Warrior>(_gameArea);
        var positionHandler = new PositionHandler<Warrior>(_gameArea.GeneratedValue);

        generatorHandler.SetNext(gameAreaValueGeneratorHandler).SetNext(positionHandler);
        
        return generatorHandler;
    }

    private void DestroyInstaller()
    {
        _timer.TimIsUp -= TryToSpawnWarrior;
        Destroy(gameObject);
    }
}

public class RandomLoopTimer : IUpdate, IDisposable
{
    public event Action TimIsUp;
    public event Action<IUpdate> UpdateRemoveRequested;

    private FloatRange _intervalRange;
    private float _currentInterval;
    private float _passedTime;
    private bool _isStopped;

    public RandomLoopTimer(FloatRange intervalRange)
    {
        _intervalRange = intervalRange;
        _currentInterval = _intervalRange.GetRandomValue();
    }

    ~RandomLoopTimer()
    {
        Dispose();
    }

    public void Resume()
    {
        _isStopped = false;
    }

    public void Stop()
    {
        _isStopped = true;
    }

    public void GameUpdate(float deltaTime)
    {
        if (_isStopped)
            return;

        _passedTime += deltaTime;

        if (_passedTime < _currentInterval)
            return;

        Reset();
    }

    private void Reset()
    {
        _passedTime = 0;
        _currentInterval = _intervalRange.GetRandomValue();
            TimIsUp?.Invoke();
    }

    public void Dispose()
    {
        UpdateRemoveRequested?.Invoke(this);
    }
}

namespace Ranges
{
}

public interface IUpdate
{
    void GameUpdate(float deltaTime);
        
    event Action<IUpdate> UpdateRemoveRequested;
}