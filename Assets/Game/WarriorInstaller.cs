using System;
using System.Collections.Generic;
using Ranges;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO : тут надо generic мутить
//todo: Алеша пж удали меня, не забудь и сделай generic
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
        
        _timer.TimIsUp += TryToSpawnWarrior;
    }

    //todo : сюда можно запихать пул чтобы была проверка на то сколько сейчас врагов на сцене 
    private void TryToSpawnWarrior()
    {
        var handler = HandlerSpawnWarrior();
        handler.Handle(_entityCreator);
    }

    private void Install()
    {
        DestroyInstaller();
    }
    
    private IHandler<Warrior> HandlerSpawnWarrior()
    {
        _entityCreator = new WarriorEntityCreator(_warriorConfig, _prefab);
        
        var generatorHandler = new EnemyGeneratorHandle<Warrior>();
        var positionHandler = new PositionHandler<Warrior>(_gameArea);
        
        generatorHandler.SetNext(positionHandler);
        
        return generatorHandler;
    }

    private void DestroyInstaller()
    {
        _timer.TimIsUp -= TryToSpawnWarrior;
        Destroy(gameObject);
    }
}

public class Generator<T> : IDisposable where T : MonoBehaviour
{
    public event Action<T> Spawned;

    private readonly IPlacer<T> _placer;
    private readonly EnityCreator<T> _entityCreator;
    private readonly RandomLoopTimer _timer; 
    
    public Generator(RandomLoopTimer timer, EnityCreator<T> entityCreator, IPlacer<T> placer)
    {
        _timer = timer;
        _entityCreator = entityCreator;
        _placer = placer;

        _timer.TimIsUp += SpawnT;
        _timer.Resume();
    }

    ~Generator()
    {
        Dispose();
    }
    
    private void SpawnT()
    {
        var entity = _entityCreator.CreateEntity();
        Spawned?.Invoke(entity);
    }

    public void Dispose()
    {
        _timer.TimIsUp -= SpawnT;
        _timer.Dispose();
    }
}

public sealed class GameArea
{
    public Vector2 Size { get; set; }
    
    private readonly Transform _leftBorder;
    private readonly Transform _rightBorder;
    private readonly Transform _topBorder;
    private readonly Transform _downBorder;

    private readonly Camera _camera;
    private readonly Vector2 _halfSize;
    
    private List<Transform> _borders;
    
    public GameArea(
        Camera camera,
        Transform leftBorder,
        Transform rightBorder, 
        Transform topBorder, 
        Transform downBorder)
    {
        _camera = camera;

        _leftBorder = leftBorder;
        _rightBorder = rightBorder;
        _topBorder = topBorder;
        _downBorder = downBorder;
        
        _halfSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
        Size = _halfSize * 2;
        
        _borders = new List<Transform>
        {
            _leftBorder,
            _rightBorder,
            _topBorder,
            _downBorder
        };
    }
    
    public Vector2 GetRandomStartPosition()
    {
        var randomValue = Random.Range(0, _borders.Count - 1);
        var startPosition = GetPosition(_borders[randomValue]);

        _borders.Remove(_borders[randomValue]);
            
        return startPosition;
    }

    public Vector2 GetRandomEndPosition()
    {
        var randomValue = Random.Range(0, _borders.Count - 1);
        var endPosition = GetPosition(_borders[randomValue]);

        return endPosition;
    }

    private Vector2 GetPosition(Transform transform)
    {
        if (transform == _leftBorder || transform == _rightBorder)
        {
            var newPosition = GetBorderYPosition(transform);
            return newPosition;
        }

        if (transform == _topBorder || transform == _downBorder)
        {
            var newPosition = GetBorderXPosition(transform);
            return newPosition;
        }

        throw new NotImplementedException("Все сломалось");
    }

    private Vector2 GetBorderXPosition(Transform transform)
    {
        var randomX = Random.Range(-_halfSize.x , _halfSize.x);

        var newPosition = new Vector2(randomX, transform.position.y);
            
        return newPosition;
    }

    private Vector2 GetBorderYPosition(Transform transform)
    {
        var randomY = Random.Range(-_halfSize.y , _halfSize.y);

        var newPosition = new Vector2(transform.position.x, randomY);

        return newPosition;
    }

    public void PlaceObject(Transform transform, Vector2 startPosition) => transform.position = startPosition;
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
    [Serializable]
    public struct FloatRange
    {
        [SerializeField, Range(0, 2000)] private float _min;
        [SerializeField, Range(0, 2000)] private float _max;

        public float Min => _min;
        public float Max => _max;

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public float GetRandomValue() => Random.Range(_min, _max);

        public static FloatRange operator +(FloatRange range1, FloatRange range2)
        {
            var max = range1.Max + range2.Max;
            var min = range1.Min + range2.Min;
            return new FloatRange(min, max);
        }
    }
}

public interface IUpdate
{
    void GameUpdate(float deltaTime);
        
    event Action<IUpdate> UpdateRemoveRequested;
}