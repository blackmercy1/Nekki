using Game.Common.Configs.Ability;
using Game.Common.Configs.Entity.Wizard;
using Game.Core.Input;
using Game.Core.Input.Updates;
using Game.Entity;
using Game.Entity.Generators;
using UnityEngine;

namespace Game.Core.Installers
{
    public sealed class WizardInstaller : MonoBehaviour
    {
        //должно быть в отдельном конфиге
        [SerializeField] private WizardConfig _wizardConfig;
        [SerializeField] private Transform _sceneStartPosition;
        [SerializeField] private RangedAbilityConfig _rangedAbilityConfig;
        [SerializeField] private FireAbilityConfig _fireAbilityConfig;

        private GameUpdates _gameUpdates;
        private IPlayerInputHandler _inputHandler;
        private WizardGenerator _wizardGenerator;
    
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
        
            _wizardGenerator = new WizardGenerator(
                _wizardConfig,
                _inputHandler,
                _gameUpdates,
                _sceneStartPosition,
                _rangedAbilityConfig,
                _fireAbilityConfig
            );
        }

        public Wizard SpawnWizard()
        {
            return _wizardGenerator.SpawnWizard();
        }

        private void DestroyInstaller()
        {
            Destroy(gameObject);
        }
    }
}