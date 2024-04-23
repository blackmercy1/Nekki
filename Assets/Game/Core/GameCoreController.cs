using Game.Common.Ranges;
using Game.Common.Timer;
using Game.Core.Input;
using Game.Core.Input.Updates;
using Game.Core.Installers;
using Game.Positions;
using UnityEngine;

//Main объект данного проекта, он являеется корневым и самым высоким в цепочки иерархии 
namespace Game.Core
{
    public class GameCoreController : MonoBehaviour
    {
        [SerializeField] private WizardInstaller _wizardInstaller;
        [SerializeField] private WarriorInstaller _warriorInstaller;
        [SerializeField] private GameUpdates _gameUpdates;

        [SerializeField] private Transform _leftTop;
        [SerializeField] private Transform _rightTop;
        [SerializeField] private Transform _leftDown;
        [SerializeField] private Transform _rightDown;
    
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
        
            _wizardInstaller.Initialize(playerInput, _gameUpdates);
            var wizard = _wizardInstaller.SpawnWizard();
            _warriorInstaller.Initialize(timer, gameArea, _gameUpdates, wizard.transform);
        }

        private IPlayerInputHandler CreatePlayerInputHandler() => new InputFromKeyboard();

        private GameArea CreateGameArea()
        {
            return new GameArea(
                _leftTop,
                _rightTop,
                _leftDown,
                _rightDown);
        }

        // не хочу тратить много времени на создание и конфигурацию десятков конфигов, поэтому
        // используются магические числа
        private RandomLoopTimer CreateTimer()
        {
            return new RandomLoopTimer(new FloatRange(1, 3)); 
        }
    }
}