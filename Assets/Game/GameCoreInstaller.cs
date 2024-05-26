using Game.Common.Ranges;
using Game.Common.Timer;
using Game.Core.Input;
using Game.Core.Updates;
using Game.Positions;
using Game.Warrior;
using Game.Wizard;
using UnityEngine;

namespace Game
{
    public class GameCoreInstaller : MonoBehaviour
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
        
            _wizardInstaller.Initialize(_gameUpdates);
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
        
        private RandomLoopTimer CreateTimer() => new(new FloatRange(1, 3));
    }
}