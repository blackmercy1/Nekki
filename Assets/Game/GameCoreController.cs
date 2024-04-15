using Ranges;
using UnityEngine;

//Main объект данного проекта, он являеется корневым и самым высоким в цепочки иерархии 
public class GameCoreController : MonoBehaviour
{
    [SerializeField] private WarriorInstaller _warriorInstaller;
    [SerializeField] private GameUpdates _gameUpdates;
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _downPoint;
    
    private void Awake()
    {
        var timer = CreateTimer();
        var gameArea = CreateGameArea();
        
        _gameUpdates.AddToUpdateList(timer);
        _warriorInstaller.Initialize(timer, gameArea);
        _gameUpdates.ResumeUpdate();
        
    }

    private GameArea CreateGameArea()
    {
        return new GameArea(
            _camera,
            _leftPoint,
            _rightPoint,
            _topPoint,
            _downPoint);
    }

    // не хочу тратить много времени на создание и конфигурацию десятков конфигов, поэтому
    // используются магические числа
    private RandomLoopTimer CreateTimer()
    {
        return new RandomLoopTimer(new FloatRange(1, 3)); 
    }
}