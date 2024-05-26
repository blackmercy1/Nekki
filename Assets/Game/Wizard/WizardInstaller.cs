using Game.Abilities.FireAbility;
using Game.Abilities.RangedAbility;
using Game.Core.Updates;
using UnityEngine;

namespace Game.Wizard
{
    public sealed class WizardInstaller : MonoBehaviour
    {
        [SerializeField] private WizardConfig _wizardConfig;
        [SerializeField] private Transform _sceneStartPosition;
        [SerializeField] private RangedAbilityConfig _rangedAbilityConfig;
        [SerializeField] private FireAbilityConfig _fireAbilityConfig;

        private GameUpdates _gameUpdates;
        private WizardEntityCreator _wizardEntityCreator;

        private bool _isInitialized = false;

        public void Initialize(GameUpdates gameUpdates)
        {
            if (_isInitialized)
                return;
            if (!_isInitialized)
                _isInitialized = !_isInitialized;

            _gameUpdates = gameUpdates;
            
            _wizardEntityCreator = new WizardEntityCreator(
                    _wizardConfig, 
                    _gameUpdates,
                    _sceneStartPosition,
                    _rangedAbilityConfig,
                    _fireAbilityConfig);
        }

        public Wizard SpawnWizard() => _wizardEntityCreator.CreateEntity();
        private void DestroyInstaller() => Destroy(gameObject);
    }
}