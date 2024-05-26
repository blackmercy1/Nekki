using Game.Common.Timer;
using Game.Core.Updates;
using Game.Entity;
using Game.Positions;
using UnityEngine;

namespace Game.Warrior
{
    public sealed class WarriorInstaller : MonoBehaviour
    {
        [SerializeField] private WarriorConfig _warriorConfig;
    
        private WarriorEntityCreator _warriorEntityCreator;
        private GameUpdates _gameUpdates;
        private GameArea _gameArea;
    
        private bool _isInitialized;
    
        public void Initialize(
            RandomLoopTimer timer,
            GameArea gameArea,
            GameUpdates gameUpdates, 
            Transform targetTransform)
        {
            if (_isInitialized)
                return;
            if (!_isInitialized)
                _isInitialized = !_isInitialized;
            
            var warriorCreator = new WarriorEntityCreator(
                _warriorConfig,
                targetTransform,
                gameUpdates,
                gameArea);
            
            var entityGenerator = new EntityGenerator<Warrior>(warriorCreator, timer);
            entityGenerator.Start();
        }
    }
}