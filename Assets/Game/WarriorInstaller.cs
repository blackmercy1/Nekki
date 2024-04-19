using System;
using Ranges;
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

public sealed class EntityGenerator : IDisposable
{
    private readonly RandomLoopTimer _timer;
    private readonly GameArea _gameArea;
    private readonly GameUpdates _gameUpdates;
    private readonly WarriorEntityCreator _warriorEntityCreator;

    public EntityGenerator(
        RandomLoopTimer timer,
        GameArea gameArea,
        GameUpdates gameUpdates,
        WarriorConfig warriorConfig)
    {
        _timer = timer;
        _gameArea = gameArea;
        _gameUpdates = gameUpdates; 
        
        _timer.TimIsUp += SpawnWarrior;
        _timer.Resume();

        _warriorEntityCreator = new WarriorEntityCreator(warriorConfig);
    }
    
    private void SpawnWarrior()
    {
        var entity = _warriorEntityCreator.CreateEntity();
        var handler = SpawnWarriorHandler();
        handler.Handle(entity);
    }
    
    private IHandler<Warrior> SpawnWarriorHandler()
    {
        var startHandler = new StartNode<Warrior>();
        var updateHandler = new AddToUpdateNode<Warrior>(_gameUpdates);
        var positionHandler = new PlaceEntityNode<Warrior>(_gameArea);

        startHandler
            .SetNext(updateHandler)
            .SetNext(positionHandler);
        
        return startHandler;
    }

    public void Dispose()
    {
        _timer.TimIsUp -= SpawnWarrior;
        _timer?.Dispose();
        _gameUpdates?.Dispose();
    }
}

public class GenerateConstantPosition : IPositionGenerator
{
    private readonly Vector3 _staticPosition;

    public GenerateConstantPosition(Vector3 staticPosition)
    {
        _staticPosition = staticPosition;
    }
    
    public Vector3 GeneratePosition()
    {
        return _staticPosition;
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

public interface IUpdate
{
    void GameUpdate(float deltaTime);
        
    event Action<IUpdate> UpdateRemoveRequested;
}