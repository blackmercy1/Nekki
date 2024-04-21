using Ranges;
using UnityEngine;

//Main объект данного проекта, он являеется корневым и самым высоким в цепочки иерархии 
public class GameCoreController : MonoBehaviour
{
    [SerializeField] private WizardInstaller _wizardInstaller;
    [SerializeField] private WarriorInstaller _warriorInstaller;
    [SerializeField] private GameUpdates _gameUpdates;
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _downPoint;
    
    private void Awake() => Install();

    private void Install()
    {
        var playerInput = CreatePlayerInputHandler();
        var gameArea = CreateGameArea();
        var timer = CreateTimer();
        
        _gameUpdates
            .Add(timer)
            .Add(playerInput);
        _gameUpdates.ResumeUpdate();
        
        _warriorInstaller.Initialize(timer, gameArea, _gameUpdates);
        _wizardInstaller.Initialize(playerInput, _gameUpdates);
    }

    private IPlayerInputHandler CreatePlayerInputHandler() => new InputFromKeyboard();

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